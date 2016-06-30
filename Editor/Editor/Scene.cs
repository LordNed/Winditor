using GameFormatReader.Common;
using System.Collections.Generic;
using System.IO;
using WindEditor.Collision;
using JStudio.J3D;

namespace WindEditor
{
    public class WScene : WDOMNode
    {
        public string Name { get; set; }

        private WWorld m_world;
        private WSkyboxNode m_skybox;

        public WScene(WWorld world)
        {
            m_world = world;
        }

        public void LoadRoom(string filePath)
        {
            Name = Path.GetFileNameWithoutExtension(filePath);

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
                            LoadFixedModelList(folder, modelNames);
                        }
                        break;
                }
            }
        }

        public void LoadStage(string sceneFolder)
        {
            Name = Path.GetFileNameWithoutExtension(sceneFolder);

            foreach (var folder in Directory.GetDirectories(sceneFolder))
            {
                string folderName = Path.GetFileNameWithoutExtension(folder);
                switch (folderName.ToLower())
                {
                    case "dzs":
                        {
                            string fileName = Path.Combine(folder, "stage.dzs");
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


        private void LoadFixedModelList(string rootFolder, string[] modelNames)
        {
            string[] extNames = new[] { ".bmd", ".bdl" };
            foreach (var model in modelNames)
            {
                foreach (var ext in extNames)
                {
                    string fullPath = Path.Combine(rootFolder, model + ext);
                    if (File.Exists(fullPath))
                    {
                        J3D roomModel = WResourceManager.LoadResource(fullPath);
                        J3DNode modelInstance = new J3DNode(roomModel);

                        roomModel.SetTevkColorOverride(0, WLinearColor.FromHexString("0xFF8C27FF"));

                        modelInstance.SetParent(this);
                    }
                }
            }
        }


        public void UnloadLevel()
        {
            throw new System.NotImplementedException();
        }

        private void LoadLevelCollisionFromFile(string filePath)
        {
            if (!File.Exists(filePath))
                return;

            var collision = new WCollisionMesh();
            using (EndianBinaryReader reader = new EndianBinaryReader(File.OpenRead(filePath), Endian.Big))
            {
                collision.Load(reader);
            }

            collision.SetParent(this);
        }

        private void LoadLevelEntitiesFromFile(string filePath)
        {
            ActorLoader actorLoader = new ActorLoader();
            List<WActorNode> loadedActors = actorLoader.LoadFromFile(filePath);
            foreach (var actor in loadedActors)
                actor.SetParent(this);
        }
    }
}
