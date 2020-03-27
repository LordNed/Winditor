using OpenTK;
using System.ComponentModel;
using System.IO;
using System;
using JStudio.J3D;
using System.Collections.Generic;
using GameFormatReader.Common;
using WArchiveTools.FileSystem;
using WindEditor.Collision;

namespace WindEditor
{
    public class WRoomTable
    {
        public struct AdjacentRoom
        {
            public bool LoadRoom { get; set; }
            public bool Unknown1 { get; set; } // No idea. Always set for the first room but not the others.
            public byte RoomIndex { get; set; }

            public AdjacentRoom(bool loadRoom, bool unknown1, byte roomIndex)
            {
                LoadRoom = loadRoom;
                Unknown1 = unknown1;
                RoomIndex = roomIndex;
            }

            public override string ToString()
            {
                return string.Format("LoadRoom: {0} Unknown1: {1} RoomIndex: {2}", LoadRoom, Unknown1, RoomIndex);
            }
        }

        public BindingList<AdjacentRoom> AdjacentRooms { get; set; }
        public byte ReverbAmount { get; set; }
        public byte TimePass { get; set; }
        public byte Unknown1 { get; set; }

        public WRoomTable()
        {
            AdjacentRooms = new BindingList<AdjacentRoom>();
        }

        public override string ToString()
        {
            return string.Format("Reverb Amount: {0} TimePass: {1} Unknown1: {2} Adjacent Room Count: {3}", ReverbAmount, TimePass, Unknown1, AdjacentRooms.Count);
        }
    }

    public class WRoomTransform
    {
        public Vector2 Translation { get; set; }
        public float YRotation { get; set; }
        public byte RoomNumber { get; set; }
        public byte Unknown1 { get; set; }

        public WRoomTransform(Vector2 translation, float yRot, byte roomIndex, byte unknown1)
        {
            Translation = translation;
            YRotation = yRot;
            RoomNumber = roomIndex;
            Unknown1 = unknown1;
        }
    }



    public class WRoom : WScene
    {
        public int RoomIndex { get; protected set; }
        public int MemoryAllocation { get; set; }
        public WRoomTransform RoomTransform { get; set; }
        public EnvironmentLightingConditions EnvironmentLighting { get; set; }

        public WRoom(WWorld world, int roomIndex):base(world)
        {
            RoomIndex = roomIndex;
            IsRendered = true;
        }

        public override void Load(string filePath)
        {
            base.Load(filePath);

            LoadRoomModels(filePath);

            foreach (var folder in Directory.GetDirectories(filePath))
            {
                string folderName = Path.GetFileNameWithoutExtension(folder);
                switch (folderName.ToLower())
                {
                    case "dzb":
                        {
                            string fileName = Path.Combine(folder, "room.dzb");
                            LoadLevelCollisionFromFile(fileName);
                        }
                        break;
                    case "dzr":
                    case "dzs":
                        {
                            string fileName = Path.Combine(folder, "room.dzr");
                            if (File.Exists(fileName))
                                LoadLevelEntitiesFromFile(fileName);
                        }
                        break;
                }
            }
        }

        private void LoadRoomModels(string filePath)
        {
            // Search the bmd and bdl folders for valid model names. Then search for a matching brk and btk for those models.
            string[] modelNames = new[] { "model", "model1", "model2", "model3" };
            string[] folderNames = new[] { "bmd", "bdl" };
            bool[] validModels = new bool[modelNames.Length];

            CategoryDOMNode col_category = new CategoryDOMNode("Models", m_world);
            col_category.SetParent(this);

            foreach (var subFolder in folderNames)
            {
                string folderPath = Path.Combine(filePath, subFolder);
                foreach (var modelName in modelNames)
                {
                    J3D mesh = LoadModel(folderPath, modelName);
                    if (mesh != null)
                    {
                        string model_path = Path.Combine(folderPath, modelName);
                        if (subFolder == "bmd")
                        {
                            model_path += ".bmd";
                        }
                        else
                        {
                            model_path += ".bdl";
                        }

                        J3DNode j3d_node = new J3DNode(mesh, m_world, model_path);
                        j3d_node.SetParent(col_category);
                    }
                }
            }
        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);
        }

        public override void SetTimeOfDay(float timeOfDay)
        {
            base.SetTimeOfDay(timeOfDay);

            List<J3DNode> m_roomModelNodes = GetChildrenOfType<J3DNode>();

            if(EnvironmentLighting != null)
            {
                var curLight = EnvironmentLighting.Lerp(EnvironmentLightingConditions.WeatherPreset.Default, true, timeOfDay);
                foreach (J3DNode node in m_roomModelNodes)
                {
                    J3D model = node.Model;
                    if(model.Name == "model")
                    {
                        model.SetTevColorOverride(0, curLight.RoomLightColor);
                        model.SetTevkColorOverride(0, curLight.RoomAmbientColor);
                    }
                    else if(model.Name == "model1")
                    {
                        model.SetTevColorOverride(0, curLight.WaveColor);
                        model.SetTevkColorOverride(0, curLight.OceanColor);
                    }
                    else if(model.Name == "model3")
                    {
                        model.SetTevColorOverride(0, curLight.DoorBackfill);
                    }
                }

                var childActors = GetChildrenOfType<VisibleDOMNode>();
                foreach(var child in childActors)
                {
                    child.ColorOverrides.SetTevColorOverride(0, curLight.ShadowColor);
                    child.ColorOverrides.SetTevkColorOverride(0, curLight.ActorAmbientColor);
                }
            }

        }

        public void SetRoomTransform(WRoomTransform roomTransform)
        {
            List<J3DNode> m_roomModelNodes = GetChildrenOfType<J3DNode>();

            RoomTransform = roomTransform;
            foreach (J3DNode j3d_node in m_roomModelNodes)
            {
                j3d_node.Transform.Position = new Vector3(RoomTransform.Translation.X, 0, RoomTransform.Translation.Y);
                j3d_node.Transform.LocalRotation = Quaternion.FromAxisAngle(Vector3.UnitY, WMath.DegreesToRadians(RoomTransform.YRotation));
            }
        }

        public Vector3 GetCenter()
        {
            List<J3DNode> m_roomModelNodes = GetChildrenOfType<J3DNode>();

            Vector3 roomOffset = Vector3.Zero;

            if (m_roomModelNodes.Count > 0)
            {
                roomOffset += m_roomModelNodes[0].Model.BoundingSphere.Center;
            }

            if (RoomTransform != null)
            {
                roomOffset += new Vector3(RoomTransform.Translation.X, 0, RoomTransform.Translation.Y);

                float angle = WMath.DegreesToRadians(-RoomTransform.YRotation);
                float origX = roomOffset.X;
                float origZ = roomOffset.Z;
                roomOffset.X = (float)(origX * Math.Cos(angle) - origZ * Math.Sin(angle));
                roomOffset.Z = (float)(origX * Math.Sin(angle) + origZ * Math.Cos(angle));
            }

            return roomOffset;
        }

        public override string ToString()
        {
            return Name;
        }

        public override void SaveEntitiesToDirectory(string directory)
        {
            string dzrDirectory = string.Format("{0}/dzr", directory);
            if (!Directory.Exists(dzrDirectory))
                Directory.CreateDirectory(dzrDirectory);

            string filePath = string.Format("{0}/room.dzr", dzrDirectory);
            using (EndianBinaryWriter writer = new EndianBinaryWriter(File.Open(filePath, FileMode.Create), Endian.Big))
            {
                SceneDataExporter exporter = new SceneDataExporter();
                exporter.ExportToStream(writer, this);
            }
        }
        public override void SaveCollisionToDirectory(string directory)
        {
            Console.WriteLine("Writing DZB File...");

            List<WCollisionMesh> meshes = GetChildrenOfType<WCollisionMesh>();
            if (meshes.Count == 0)
                return;

            foreach (WCollisionMesh m in meshes)
            {
                string dzbDirectory = string.Format("{0}/dzb", directory);
                if (!Directory.Exists(dzbDirectory))
                    Directory.CreateDirectory(dzbDirectory);

                string filePath = string.Format("{0}/room.dzb", dzbDirectory);
                using (EndianBinaryWriter writer = new EndianBinaryWriter(File.Open(filePath, FileMode.Create), Endian.Big))
                {
                    writer.Write(m.ToDZBArray());
                }
            }

            Console.WriteLine("Finished saving DZB File.");
        }

        public override VirtualFilesystemDirectory ExportToVFS()
        {
            VirtualFilesystemDirectory new_dir = SourceDirectory;
            new_dir.Name = Name;

            VirtualFilesystemFile dzr_file = SourceDirectory.GetFileAtPath("dzr/room.dzr");

            using (MemoryStream mem = new MemoryStream())
            {
                using (EndianBinaryWriter writer = new EndianBinaryWriter(mem, Endian.Big))
                {
                    SceneDataExporter exporter = new SceneDataExporter();
                    exporter.ExportToStream(writer, this);

                    writer.Flush();

                    dzr_file.Data = mem.ToArray();
                }
            }

            List<WCollisionMesh> colMeshes = GetChildrenOfType<WCollisionMesh>();

            foreach (WCollisionMesh m in colMeshes)
            {
                VirtualFilesystemFile dzb_file = SourceDirectory.GetFileAtPath("dzb/room.dzb");

                using (MemoryStream mem = new MemoryStream())
                {
                    using (EndianBinaryWriter writer = new EndianBinaryWriter(mem, Endian.Big))
                    {
                        writer.Write(m.ToDZBArray());
                        dzb_file.Data = mem.ToArray();
                    }
                }
            }

            List<J3DNode> meshes = GetChildrenOfType<J3DNode>();

            for (int i = 0; i < meshes.Count; i++)
            {
                string modelExt = meshes[i].Model.StudioType == "bdl4" ? "bdl" : "bmd";

                VirtualFilesystemFile modelFile = SourceDirectory.GetFileAtPath($"{ modelExt }/{ meshes[i].Name }.{ modelExt }");
                byte[] data = File.ReadAllBytes(meshes[i].Filename);

                if (modelFile != null)
                {
                    modelFile.Data = data;
                }
                else
                {
                    VirtualFilesystemDirectory modelDir = null;

                    foreach (VirtualFilesystemNode n in new_dir.Children)
                    {
                        if (n.Name == modelExt)
                        {
                            modelDir = n as VirtualFilesystemDirectory;
                            break;
                        }
                    }

                    if (modelDir == null)
                    {
                        modelDir = new VirtualFilesystemDirectory(modelExt);
                        new_dir.Children.Add(modelDir);
                    }

                    modelDir.Children.Add(new VirtualFilesystemFile(meshes[i].Name, $".{ modelExt }", data));
                }
            }

            return new_dir;
        }

        public void ApplyTransformToObjects()
        {
            SetRoomTransform(RoomTransform);
            Vector3 offset = new Vector3(RoomTransform.Translation.X, 0, RoomTransform.Translation.Y);

            List<VisibleDOMNode> objects = GetChildrenOfType<VisibleDOMNode>();

            foreach (VisibleDOMNode n in objects)
            {
                n.Transform.Position += offset;

                if (n is SpawnPoint)
                {
                    SpawnPoint spwn = n as SpawnPoint;
                    spwn.Room = (byte)RoomIndex;
                }
            }

            List<WCollisionMesh> colList = GetChildrenOfType<WCollisionMesh>();

            if (colList.Count > 0)
                colList[0].ApplyRoomTransform(offset, RoomIndex);
        }
    }
}
