using System.ComponentModel;
using System.IO;

namespace WindEditor
{
    public class RoomTable
    {
        public struct AdjacentRoom
        {
            public bool Unknown0 { get; set; } // Start Visible?
            public bool Unknown1 { get; set; } // No idea. Always set for the first room but not the others.
            public byte RoomIndex { get; set; }

            public AdjacentRoom(bool unknown0, bool unknown1, byte roomIndex)
            {
                Unknown0 = unknown0;
                Unknown1 = unknown1;
                RoomIndex = roomIndex;
            }
        }

        public BindingList<AdjacentRoom> AdjacentRooms { get; set; }
        public byte TimePass { get; set; }
        public byte Unknown0 { get; set; }
        public byte Unknown1 { get; set; }
    }

    public class WRoom : WScene
    {
        public WRoom(WWorld world):base(world)
        {

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
