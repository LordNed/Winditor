using GameFormatReader.Common;
using System.Collections.Generic;
using System.IO;
using WindEditor.Collision;
using JStudio.J3D;

namespace WindEditor
{
    public class WScene : WDOMNode
    {
        public string Name { get; protected set; }

        protected WWorld m_world;

        public WScene(WWorld world)
        {
            m_world = world;
        }

        public virtual void Load(string filePath)
        {
            Name = Path.GetFileNameWithoutExtension(filePath);
        }

        protected virtual J3D LoadModel(string rootFolder, string modelName)
        {
            string[] extNames = new[] { ".bmd", ".bdl" };
            foreach (var ext in extNames)
            {
                string fullPath = Path.Combine(rootFolder, modelName + ext);
                if (File.Exists(fullPath))
                {
                    J3D j3dMesh = WResourceManager.LoadResource(fullPath);
                    return j3dMesh;
                }
            }

            return null;
        }


        public virtual void UnloadLevel()
        {
            throw new System.NotImplementedException();
        }

        protected virtual void LoadLevelCollisionFromFile(string filePath)
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

        protected virtual void LoadLevelEntitiesFromFile(string filePath)
        {
            SceneDataLoader actorLoader = new SceneDataLoader(filePath);
            List<WActorNode> loadedActors = actorLoader.GetMapEntities();
            foreach (var actor in loadedActors)
                actor.SetParent(this);
        }
    }
}
