using System.ComponentModel;
using System.IO;

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

    public class WRoom : WScene
    {
        public int RoomIndex { get; protected set; }
        public WRoomTable RoomTable { get; set; }

        public WRoom(WWorld world, int roomIndex):base(world)
        {
            RoomIndex = roomIndex;
        }

        public override void Load(string filePath)
        {
            base.Load(filePath);

            foreach (var folder in Directory.GetDirectories(filePath))
            {
                string folderName = Path.GetFileNameWithoutExtension(folder);
                switch (folderName.ToLower())
                {
                    case "dzb":
                        {
                            string fileName = Path.Combine(folder, "room.dzb");
                            //LoadLevelCollisionFromFile(fileName);
                        }
                        break;
                    case "dzr":
                    case "dzs":
                        {
                            string fileName = Path.Combine(folder, "room.dzr");
                            if (!File.Exists(fileName))
                                fileName = Path.Combine(folder, "stage.dzs");
                            LoadLevelEntitiesFromFile(fileName);
                        }
                        break;
                    case "bmd":
                    case "bdl":
                        {
                            // Wind Waker has a fixed list of models that it can load from the bmd/bdl folder of a given room:
                            // model, model1, model2, model3. 
                            string[] modelNames = new[] { "model", "model1", "model2", "model3" };
                            foreach(var model in modelNames)
                                LoadFixedModelList(folder, model);
                        }
                        break;
                }
            }
        }
    }
}
