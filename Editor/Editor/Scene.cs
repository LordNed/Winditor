using GameFormatReader.Common;
using System.Collections.Generic;
using System.IO;
using WindEditor.Collision;
using System;
using JStudio.J3D;

namespace WindEditor
{
    public class WScene : WDOMNode
    {
        //public List<IRenderable> RenderableObjects { get { return m_renderableObjects; } }
        public string Name { get; set; }

        //private List<IRenderable> m_renderableObjects;
        //private List<ITickableObject> m_tickableObjects;

        private WWorld m_world;
        //private List<J3D> m_roomModels;

        public WScene(WWorld world)
        {
            m_world = world;
            //m_renderableObjects = new List<IRenderable>();
            //m_tickableObjects = new List<ITickableObject>();
            //m_roomModels = new List<J3D>();
        }

        public void LoadLevel(string filePath)
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
                            // Load the room model for this room.
                            LoadRoomModel(folder);

                            // Load models for the skybox in this scene if they exist.
                            LoadSkyboxModels(folder);

                            // Load our fixed list of doors, etc.
                        }
                        break;
                }
            }
        }

        private void LoadRoomModel(string rootFolder)
        {
            // Wind Waker has a fixed list of models that it can load from the bmd/bdl folder of a given room:
            // model, model1, model2, model3. 
            string[] modelNames = new[] { "model", "model1", "model2", "model3" };
            LoadFixedModelList(rootFolder, modelNames);
        }

        private void LoadSkyboxModels(string rootFolder)
        {
            // There's a fixed list of skybox models and order.
            string[] fileNames = new[] { "vr_sky", "vr_kasumi_mae", "vr_uso_umi", "vr_back_cloud" };
            LoadFixedModelList(rootFolder, fileNames);
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
                        Children.Add(modelInstance);
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

            RegisterObject(collision);
            Children.Add(collision);
        }

        private void LoadLevelEntitiesFromFile(string filePath)
        {
            ActorLoader actorLoader = new ActorLoader();
            List<WActorNode> loadedActors = actorLoader.LoadFromFile(filePath);
            foreach (var actor in loadedActors)
                Children.Add(actor);
        }

        public void RegisterObject(object obj)
        {
            // This is awesome.
            if (obj is IRenderable)
            {
                //m_renderableObjects.Add(obj as IRenderable);
            }

            if (obj is ITickableObject)
            {
                ITickableObject tickableObj = (ITickableObject)obj;
                tickableObj.SetWorld(m_world);
                tickableObj.SetScene(this);
                //m_tickableObjects.Add(tickableObj);
            }

            if (obj is IUndoable)
            {
                IUndoable undoable = obj as IUndoable;
                undoable.SetUndoStack(m_world.UndoStack);
            }
        }

        public void UnregisterObject(object obj)
        {
            if (obj is IRenderable)
            {
                IRenderable renderable = obj as IRenderable;
                renderable.ReleaseResources();

                //m_renderableObjects.Remove(renderable);
            }

            if (obj is ITickableObject)
            {
                //m_tickableObjects.Remove(obj as ITickableObject);
            }
        }

        public void ProcessTick(float deltaTime)
        {
            foreach (var child in Children)
                child.Tick(deltaTime);
        }
    }
}
