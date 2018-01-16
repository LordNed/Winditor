using GameFormatReader.Common;
using System.Collections.Generic;
using System.IO;
using WindEditor.Collision;
using JStudio.J3D;
using System;

namespace WindEditor
{
    public abstract class WScene : WDOMNode
    {
        public string Name { get; protected set; }

		private Dictionary<string, DOMGroupNode> m_fourCCGroups;

		public WScene(WWorld world) : base(world)
        {
			m_fourCCGroups = new Dictionary<string, DOMGroupNode>();
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

            Console.WriteLine(Path.GetFileName(filePath));
            List<WActorNode> loadedActors = actorLoader.GetMapEntities();
            foreach (var actor in loadedActors)
                actor.SetParent(this);

			foreach (var child in GetChildrenOfType<WActorNode>())
			{
				if (!m_fourCCGroups.ContainsKey(child.FourCC))
				{
					m_fourCCGroups[child.FourCC] = new DOMGroupNode(child.FourCC, m_world);
					m_fourCCGroups[child.FourCC].SetParent(this);
				}

				child.SetParent(m_fourCCGroups[child.FourCC]);
			}
		}

        public virtual void SaveToDirectory(string directory)
        {
            if(!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            Console.WriteLine("Saving {0} to {1}...", Name, directory);

            Console.WriteLine("Writing DZR/DZS File...");
            SaveEntitiesToDirectory(directory);
            Console.WriteLine("Finished saving DZR/DZS File.");


            Console.WriteLine("Finished Saving {0}.", Name);
        }

        public abstract void SaveEntitiesToDirectory(string directory);
    }
}
