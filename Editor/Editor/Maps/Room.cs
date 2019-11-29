using OpenTK;
using System.ComponentModel;
using System.IO;
using System;
using JStudio.J3D;
using System.Collections.Generic;
using GameFormatReader.Common;
using WArchiveTools.FileSystem;

namespace WindEditor.a
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

            WDOMOrganizerNode col_category = new WDOMOrganizerNode(World, typeof(WDOMOrganizerNode), "Models");
            col_category.SetParent(this);

            foreach (var subFolder in folderNames)
            {
                string folderPath = Path.Combine(filePath, subFolder);
                foreach (var modelName in modelNames)
                {
                    LightingType light_type = LightingType.Room;

                    switch (modelName)
                    {
                        // model is always the main room model, so it uses the Room lighting type.
                        case "model":
                            light_type = LightingType.Room;
                            break;
                        // model1 is usually reserved for water, so it uses the water colors.
                        case "model1":
                            light_type = LightingType.Water;
                            break;
                        // model2 is has no defined purpose, but it has a section in the lighing block.
                        case "model2":
                            light_type = LightingType.Exterior;
                            break;
                        // model3 is usually for door and window backfills.
                        case "model3":
                            light_type = LightingType.Backfill;
                            break;
                    }

                    J3D mesh = LoadModel(folderPath, modelName);
                    if (mesh != null)
                    {
                        WDOMJ3DRenderNode room_model = new WDOMJ3DRenderNode(World, mesh.Name)
                        {
                            Renderable = new J3DRenderable(mesh, null, light_type)
                        };

                        room_model.SetParent(col_category);
                    }
                }
            }
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

            return new_dir;
        }
    }
}
