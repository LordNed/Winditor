using GameFormatReader.Common;
using System.Collections.Generic;
using System.IO;
using WindEditor.Collision;

namespace WindEditor
{
    public class WScene
    {
        public List<IRenderable> RenderableObjects { get { return m_renderableObjects; } }

        private List<IRenderable> m_renderableObjects;
        private List<ITickableObject> m_tickableObjects;

        private WWorld m_world;

        public WScene(WWorld world)
        {
            m_world = world;
            m_renderableObjects = new List<IRenderable>();
            m_tickableObjects = new List<ITickableObject>();
        }

        public void LoadLevel(string filePath)
        {
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
                    case "dzs:":
                        {
                            string fileName = Path.Combine(folder, "room.dzr");
                            if (!File.Exists(fileName))
                                fileName = Path.Combine(folder, "stage.dzs");
                            LoadLevelEntitiesFromFile(fileName);
                        }
                        break;
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
        }

        private void LoadLevelEntitiesFromFile(string filePath)
        {
            ActorLoader actorLoader = new ActorLoader();
            var loadedActors = actorLoader.LoadFromFile(filePath);
            foreach (var actor in loadedActors)
                RegisterObject(actor);
        }

        public void RegisterObject(object obj)
        {
            // This is awesome.
            if (obj is IRenderable)
            {
                m_renderableObjects.Add(obj as IRenderable);
            }

            if (obj is ITickableObject)
            {
                ITickableObject tickableObj = (ITickableObject)obj;
                tickableObj.SetWorld(m_world);
                m_tickableObjects.Add(tickableObj);
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

                m_renderableObjects.Remove(renderable);
            }

            if (obj is ITickableObject)
            {
                m_tickableObjects.Remove(obj as ITickableObject);
            }
        }

        public void ProcessTick(float deltaTime)
        {
            foreach (var item in m_tickableObjects)
            {
                item.Tick(deltaTime);
            }
        }
    }
}
