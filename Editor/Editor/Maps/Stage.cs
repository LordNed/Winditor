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
                            m_skybox = new WSkyboxNode(m_world);
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
                SceneDataLoader sceneData = new SceneDataLoader(dzsFilePath, m_world);
                // Load Room Translation info. Wind Waker stores collision and entities in world-space coordinates,
                // but models all of their rooms around 0,0,0. To solve this, there is a chunk labeled "MULT" which stores
                // the room model's translation and rotation.
                var multTable = sceneData.GetRoomTransformTable();
                if (mapRooms.Count != multTable.Count)
                    Console.WriteLine("WStage: Mismatched number of entries in Mult Table ({0}) and number of loaded rooms ({1})!", multTable.Count, mapRooms.Count);

                for(int i = 0; i < multTable.Count; i++)
                {
                    WRoom room = mapRooms.Find(x => x.RoomIndex == multTable[i].RoomNumber);
                    if (room != null)
                        room.RoomTransform = multTable[i];
                }

                // Load Room Memory Allocation info. How much extra memory do these rooms allocate?
                var allocTable = sceneData.GetRoomMemAllocTable();
                if (mapRooms.Count != allocTable.Count)
                    Console.WriteLine("WStage: Mismatched number of entries in Meco Table ({0}) and number of loaded rooms ({1})!", allocTable.Count, mapRooms.Count);

                for(int i = 0; i < allocTable.Count; i++)
                {
                    WRoom room = mapRooms.Find(x => allocTable[i].RoomIndex == x.RoomIndex);
                    if (room != null)
                        room.MemoryAllocation = allocTable[i].MemorySize;
                }
            }
        }
    }
}
