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

        public WScene(WWorld world) : base(world)
        {
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

                    // Now that we've loaded a j3dMesh, we're going to try loading btk anims too.
                    string btkFolder = rootFolder + "\\..\\btk\\";
                    string btkFile = btkFolder + modelName + ".btk";

                    if (File.Exists(btkFile))
                    {
                        j3dMesh.LoadMaterialAnim(btkFile);
                        j3dMesh.SetMaterialAnimation(modelName);
                    }

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

            var collision = new WCollisionMesh(m_world);
            using (EndianBinaryReader reader = new EndianBinaryReader(File.OpenRead(filePath), Endian.Big))
            {
                collision.Load(reader);
            }

            collision.SetParent(this);
        }

        protected virtual void LoadLevelEntitiesFromFile(string filePath)
        {
            SceneDataLoader actorLoader = new SceneDataLoader(filePath, m_world);
            List<WActorNode> loadedActors = actorLoader.GetMapEntities();
            foreach (var actor in loadedActors)
                actor.SetParent(this);
        }
    }
}
