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

        private string[] m_fourCCs;
        private Dictionary<string, DOMGroupNode> m_fourCCGroups;

        public WScene(WWorld world) : base(world)
        {
            m_fourCCGroups = new Dictionary<string, DOMGroupNode>();
            m_fourCCs = new string[] { "ACTR", "ACT0", "ACT1", "ACT2", "ACT3", "ACT4", "ACT5", "ACT6", "ACT7", "ACT8", "ACT9", "ACTa", "ACTb",
                                       "SCOB", "SCO0", "SCO1", "SCO2", "SCO3", "SCO4", "SCO5", "SCO6", "SCO7", "SCO8", "SCO9", "SCOa", "SCOb",
                                       "TRES", "TRE0", "TRE1", "TRE2", "TRE3", "TRE4", "TRE5", "TRE6", "TRE7", "TRE8", "TRE9", "TREa", "TREb",
                                       "SCLS", "RPAT", "PATH", "RPPN", "PPNT", "RARO", "STAG", "EVNT", "FILI", "LGHT", "LGTV", "LBNK", "MECO",
                                       "MEMA", "MULT", "RTBL", "PLYR", "SHIP", "SOND", "2DMA", "CAMR", "RCAM", "EnvR", "Colo", "Pale", "Virt",
                                       "DMAP", "FLOR", "DOOR", "TGDR", "TGSC", "AROB", "TGOB" };

            foreach (string str in m_fourCCs)
            {
                if (!str.Contains("ACT") && !str.Contains("SCO") && !str.Contains("TRE"))
                    m_fourCCGroups[str] = new DOMGroupNode(str, m_world);
            }

            m_fourCCGroups["Actors"] = new DOMGroupNode("Actors", m_world);
            m_fourCCGroups["Actors"].Children.Add(new DOMGroupNode("ACTR", m_world));

            m_fourCCGroups["Scaleable Objects"] = new DOMGroupNode("Scaleable Objects", m_world);
            m_fourCCGroups["Scaleable Objects"].Children.Add(new DOMGroupNode("SCOB", m_world));

            m_fourCCGroups["Treasure Chests"] = new DOMGroupNode("Treasure Chests", m_world);
            m_fourCCGroups["Treasure Chests"].Children.Add(new DOMGroupNode("TRES", m_world));

            for (int i = 0; i < 12; i++)
            {
                m_fourCCGroups["Actors"].Children.Add(new DOMGroupNode($"ACT{ i.ToString("x") }", m_world));
                m_fourCCGroups["Scaleable Objects"].Children.Add(new DOMGroupNode($"SCO{ i.ToString("x") }", m_world));
                m_fourCCGroups["Treasure Chests"].Children.Add(new DOMGroupNode($"TRE{ i.ToString("x") }", m_world));
            }
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
            List<WDOMNode> loadedActors = actorLoader.GetMapEntities();
            foreach (var actor in loadedActors)
                actor.SetParent(this);

            foreach (var child in GetChildrenOfType<WActorNode>())
            {
                if (child.FourCC.Contains("ACT"))
                {
                    child.SetParent(m_fourCCGroups["Actors"].Children[(int)child.Layer]);
                }
                else if (child.FourCC.Contains("SCO"))
                {
                    child.SetParent(m_fourCCGroups["Scaleable Objects"].Children[(int)child.Layer]);
                }
                else if (child.FourCC.Contains("TRE"))
                {
                    child.SetParent(m_fourCCGroups["Treasure Chests"].Children[(int)child.Layer]);
                }
                else
                {
                    child.SetParent(m_fourCCGroups[child.FourCC]);
                }

                child.IsVisible = true;
            }

            List<KeyValuePair<string, string>> dispFourCCs = new List<KeyValuePair<string, string>>();
            foreach (var item in m_fourCCGroups)
            {
                dispFourCCs.Add(new KeyValuePair<string, string>(item.Value.ToString(), item.Key));
            }

            // Sort the FourCCs alphabetically by their ToString() value
            for (int i = 0; i < dispFourCCs.Count; i++)
            {
                for (int j = i; j < dispFourCCs.Count; j++)
                {
                    if (dispFourCCs[i].Key.CompareTo(dispFourCCs[j].Key) > 0)
                    {
                        KeyValuePair<string, string> temp = dispFourCCs[i];
                        dispFourCCs[i] = dispFourCCs[j];
                        dispFourCCs[j] = temp;
                    }
                }
            }

            // Add entities to the DOM in the sorted order
            foreach (KeyValuePair<string, string> keyVal in dispFourCCs)
            {
                m_fourCCGroups[keyVal.Value].SetParent(this);
            }
        }

        public virtual void SaveToDirectory(string directory)
        {
            if (!Directory.Exists(directory))
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
