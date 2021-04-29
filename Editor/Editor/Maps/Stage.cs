using GameFormatReader.Common;
using System;
using System.Collections.Generic;
using System.IO;
using WArchiveTools.FileSystem;
using JStudio.J3D;
using WindEditor.Events;

namespace WindEditor
{
    public class WStage : WScene
    {
        private WSkyboxNode m_skybox;

        public WStage(WWorld world) : base(world)
        {
            IsRendered = true;
        }

        public override void Load(string filePath)
        {
            base.Load(filePath);

            foreach (var folder in Directory.GetDirectories(filePath))
            {
                string folderName = Path.GetFileNameWithoutExtension(folder);
                switch (folderName.ToLower())
                {
                    case "dzs":
                        {
                            string fileName = Path.Combine(folder, "stage.dzs");
                            if (File.Exists(fileName))
                                LoadLevelEntitiesFromFile(fileName);
                        }
                        break;
                    //case "bmd":
                    case "bdl":
                        {
                            LoadStageModels(folder);
                        }
                        break;
                    case "dat":
                        {
                            string fileName = Path.Combine(folder, "event_list.dat");
                            if (File.Exists(fileName))
                            {
                                WEventList evlist = new WEventList(m_world, fileName);

                                CategoryDOMNode evCategory = new CategoryDOMNode("Event List", m_world);
                                evCategory.SetParent(this);

                                evlist.SetParent(evCategory);
                            }
                        }
                        break;
                }
            }
        }

        private void LoadStageModels(string filepath)
        {
            m_skybox = new WSkyboxNode(m_world);
            m_skybox.LoadSkyboxModelsFromFixedModelList(filepath);
            m_skybox.SetParent(this);

            CategoryDOMNode meshCategory = new CategoryDOMNode("Models", m_world);
            meshCategory.SetParent(this);

            string[] files = Directory.GetFiles(filepath, "*.bdl");

            foreach (string str in files)
            {
                J3D mesh = LoadModel(filepath, Path.GetFileNameWithoutExtension(str));
                if (mesh != null)
                {
                    J3DNode j3d_node = new J3DNode(mesh, m_world, str);
                    j3d_node.IsRendered = false;
                    j3d_node.SetParent(meshCategory);
                }
            }
        }

        public void PostLoadProcessing(string mapDirectory, List<WRoom> mapRooms)
        {
            string dzsFilePath = Path.Combine(mapDirectory, "Stage/dzs/stage.dzs");
            if (File.Exists(dzsFilePath))
            {
                SceneDataLoader sceneData = new SceneDataLoader(dzsFilePath, m_world);
                // Load Room Translation info. Wind Waker stores collision and entities in world-space coordinates,
                // but models all of their rooms around 0,0,0. To solve this, there is a chunk labeled "MULT" which stores
                // the room model's translation and rotation.
                var multTable = sceneData.GetRoomTransformTable();
                if (mapRooms.Count != multTable.Count)
                    Console.WriteLine("WStage: Mismatched number of entries in Mult Table ({0}) and number of loaded rooms ({1})!", multTable.Count, mapRooms.Count);

                for (int i = 0; i < multTable.Count; i++)
                {
                    WRoom room = mapRooms.Find(x => x.RoomIndex == multTable[i].RoomNumber);
                    if (room != null)
                    {
                        room.SetRoomTransform(multTable[i]);
                    }
                }

                // Load Room Memory Allocation info. How much extra memory do these rooms allocate?
                var allocTable = sceneData.GetRoomMemAllocTable();
                if (mapRooms.Count != allocTable.Count)
                    Console.WriteLine("WStage: Mismatched number of entries in Meco Table ({0}) and number of loaded rooms ({1})!", allocTable.Count, mapRooms.Count);

                for (int i = 0; i < allocTable.Count; i++)
                {
                    WRoom room = mapRooms.Find(x => allocTable[i].RoomIndex == x.RoomIndex);
                    if (room != null)
                        room.MemoryAllocation = allocTable[i].MemorySize;
                }

                // Extract our EnvR data.
                var envrData = GetChildrenOfType<EnvironmentLightingConditions>();
				
                // This doesn't always match up, as sea has 52 EnvR entries but only 50 rooms, but meh.
                if (mapRooms.Count != envrData.Count)
                {
                    Console.WriteLine("WStage: Mismatched number of entries in Envr ({0}) and number of loaded rooms ({1})!",
                        envrData.Count, mapRooms.Count);
                }
				
                if (envrData.Count > 0)
                {
                    foreach (var room in mapRooms)
                    {
                        room.EnvironmentLighting = envrData[0];
                    }
                }

                for (int i = 0; i < envrData.Count; i++)
                {
                    WRoom room = mapRooms.Find(x => x.RoomIndex == i);
                    if (room != null)
                        room.EnvironmentLighting = envrData[i];
                }
            }
        }

        public override void SetTimeOfDay(float timeOfDay)
        {
            base.SetTimeOfDay(timeOfDay);

            if (m_skybox == null)
                return;

            WRoom first_room = null;

            foreach (var node in m_world.Map.SceneList)
            {
                if (node is WRoom)
                {
                    first_room = (WRoom)node;
                    break;
                }
            }

            if (first_room == null || first_room.EnvironmentLighting == null)
                return;

            var envrData = GetChildrenOfType<EnvironmentLightingConditions>();

            var curLight = envrData[0].Lerp(EnvironmentLightingConditions.WeatherPreset.Default, true, timeOfDay);

            m_skybox.SetColors(curLight.SkyboxPalette);


            // Set color overrides for certain stage actors that have parameters specifying what room they belong in.
            var childActors = GetChildrenOfType<VisibleDOMNode>();
            foreach (var child in childActors)
            {
                var possibleRoomNums = new List<int>();

                if (child is tbox)
                {
                    var chest = child as tbox;
                    possibleRoomNums.Add(chest.RoomNumber);
                }
                else if (child is door10)
                {
                    var door = child as door10;
                    possibleRoomNums.Add(door.FrontRoomNumber);
                    possibleRoomNums.Add(door.BackRoomNumber);
                }
                else if (child is door12)
                {
                    var door = child as door12;
                    possibleRoomNums.Add(door.FromRoomNumber);
                    possibleRoomNums.Add(door.ToRoomNumber);
                }

                if (possibleRoomNums.Count == 0)
                    continue;

                WRoom containingRoom = null;
                foreach (var node in m_world.Map.SceneList)
                {
                    if (node is WRoom)
                    {
                        WRoom room = node as WRoom;
                        if (possibleRoomNums.Contains(room.RoomIndex))
                        {
                            containingRoom = room;
                        }
                    }
                }

                if (containingRoom == null)
                    continue;

                var containingRoomLight = containingRoom.EnvironmentLighting.Lerp(EnvironmentLightingConditions.WeatherPreset.Default, true, timeOfDay);
                child.ColorOverrides.SetTevColorOverride(0, containingRoomLight.ShadowColor);
                child.ColorOverrides.SetTevkColorOverride(0, containingRoomLight.ActorAmbientColor);
            }
        }

        public override void SaveToDirectory(string directory)
        {
            base.SaveToDirectory(directory);

            Console.WriteLine("Writing event_list.dat...");
            SaveEventListToDirectory(directory);
            Console.WriteLine("Finished saving event_list.dat.");
        }

        public override void SaveEntitiesToDirectory(string directory)
        {
            string dzsDirectory = string.Format("{0}/dzs", directory);
            if (!Directory.Exists(dzsDirectory))
                Directory.CreateDirectory(dzsDirectory);

            string filePath = string.Format("{0}/stage.dzs", dzsDirectory);
            using (EndianBinaryWriter writer = new EndianBinaryWriter(File.Open(filePath, FileMode.Create), Endian.Big))
            {
                SceneDataExporter exporter = new SceneDataExporter();
                exporter.ExportToStream(writer, this);
            }
        }

        public override void SaveCollisionToDirectory(string directory)
        {
        }

        public void SaveEventListToDirectory(string directory)
        {
            string datDirectory = string.Format("{0}/dat", directory);
            if (!Directory.Exists(datDirectory))
                Directory.CreateDirectory(datDirectory);

            WEventList eventlist = GetChildrenOfType<WEventList>()[0];

            string filePath = string.Format("{0}/event_list.dat", datDirectory);
            using (EndianBinaryWriter writer = new EndianBinaryWriter(File.Open(filePath, FileMode.Create), Endian.Big))
            {
                eventlist.ExportToStream(writer);
            }
        }

        public override VirtualFilesystemDirectory ExportToVFS()
        {
            VirtualFilesystemDirectory new_dir = SourceDirectory;
            new_dir.Name = Name;

            VirtualFilesystemFile dzs_file = SourceDirectory.GetFileAtPath("dzs/stage.dzs");

            using (MemoryStream mem = new MemoryStream())
            {
                using (EndianBinaryWriter writer = new EndianBinaryWriter(mem, Endian.Big))
                {
                    SceneDataExporter exporter = new SceneDataExporter();
                    exporter.ExportToStream(writer, this);

                    writer.Flush();

                    dzs_file.Data = mem.ToArray();
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

            VirtualFilesystemFile dat_file = SourceDirectory.GetFileAtPath("dat/event_list.dat");
            WEventList eventlist = GetChildrenOfType<WEventList>()[0];

            using (MemoryStream ev_strm = new MemoryStream())
            {
                using (EndianBinaryWriter writer = new EndianBinaryWriter(ev_strm, Endian.Big))
                {
                    eventlist.ExportToStream(writer);
                    writer.Flush();

                    dat_file.Data = ev_strm.ToArray();
                }
            }

            return new_dir;
        }

        public override string ToString()
        {
            return Name;
        }

        public override void SaveModelsToDirectory(string directory)
        {
            base.SaveModelsToDirectory(directory);

            // Save skybox models
            List<WSkyboxNode> skyboxList = GetChildrenOfType<WSkyboxNode>();
            if (skyboxList.Count > 0)
            {
                string finalDir = Path.Combine(directory, "bdl");
                skyboxList[0].SaveToDirectory(finalDir);
            }
        }
    }
}
