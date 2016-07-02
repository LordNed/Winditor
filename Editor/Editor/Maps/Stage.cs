using System;
using System.Collections.Generic;
using System.IO;

namespace WindEditor
{
    public class WStage : WScene
    {
        private WSkyboxNode m_skybox;

        public WStage(WWorld world):base(world)
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
                    case "dzs":
                        {
                            string fileName = Path.Combine(folder, "stage.dzs");
                            if(File.Exists(fileName))
                                LoadLevelEntitiesFromFile(fileName);
                        }
                        break;
                    case "bmd":
                    case "bdl":
                        {
                            m_skybox = new WSkyboxNode();
                            m_skybox.LoadSkyboxModelsFromFixedModelList(folder);
                            m_skybox.SetParent(this);
                        }
                        break;
                }
            }
        }

        public void PostLoadProcessing(string mapDirectory, List<WRoom> mapRooms)
        {
            string dzsFilePath = Path.Combine(mapDirectory, "Stage/dzs/stage.dzs");
            if(File.Exists(dzsFilePath))
            {
                SceneDataLoader sceneData = new SceneDataLoader(dzsFilePath);
                var roomTable = sceneData.GetRoomTable();

                if(mapRooms.Count != roomTable.Count)
                    Console.WriteLine("WStage: Mismatched number of entries in roomTable ({0}) and number of loaded rooms ({1})!", roomTable.Count, mapRooms.Count);

                for(int i = 0; i < roomTable.Count; i++)
                {
                    WRoom room = mapRooms.Find(x => x.RoomIndex == i);
                    if (room != null)
                        room.RoomTable = roomTable[i];
                }
            }
        }
    }
}
