using GameFormatReader.Common;
using System.Collections.Generic;
using System.IO;
using WindEditor.Collision;
using JStudio.J3D;
using System;
using WArchiveTools.FileSystem;

namespace WindEditor
{
    public abstract class WScene : WDOMNode
    {
        override public string Name { get; set; }
        public VirtualFilesystemDirectory SourceDirectory { get; set; }

        protected Dictionary<FourCC, WDOMNode> m_fourCCGroups;

        public WScene(WWorld world) : base(world)
        {
            m_fourCCGroups = new Dictionary<FourCC, WDOMNode>();

            // We're going to iterate through the enum values to create DOM nodes for them.
            // We're skipping all of the actors, scaleable objects, and treasure chests though, because they're special.
            foreach (FourCC f in Enum.GetValues(typeof(FourCC)))
            {
                // Skip Actors/Scaleable Objects/Treasure Chests
                if (f.ToString().Contains("ACT") || f.ToString().Contains("SCO") || f.ToString().Contains("TRE") || f == FourCC.NONE)
                {
                    continue;
                }

                m_fourCCGroups[f] = new WDOMGroupNode(f, m_world);
            }

            // To handle the fact that actors/scaleable/treasure chests have layers, we're going to create DOM nodes using
            // the default layer's FourCC (ACTR/SCOB/TRES). This DOM node won't interact directly with the entities, rather
            // it will be the parent node of the nodes that do. WDOMGroupNode.ToString() is overridden to return a more general
            // description of them ("Actors", etc) instead of the FourCC's FourCCConversion.GetDescriptionFromEnum() value.
            m_fourCCGroups[FourCC.ACTR] = new WDOMGroupNode(FourCC.ACTR, m_world);
            m_fourCCGroups[FourCC.SCOB] = new WDOMGroupNode(FourCC.SCOB, m_world);
            m_fourCCGroups[FourCC.TRES] = new WDOMGroupNode(FourCC.TRES, m_world);

            // Now we add the default layer for each object type. WDOMLayeredGroupNode directly interacts with the entities.
            WDOMLayeredGroupNode actrDefLayer = new WDOMLayeredGroupNode(FourCC.ACTR, MapLayer.Default, m_world);
            actrDefLayer.SetParent(m_fourCCGroups[FourCC.ACTR]);

            WDOMLayeredGroupNode scobDefLayer = new WDOMLayeredGroupNode(FourCC.SCOB, MapLayer.Default, m_world);
            scobDefLayer.SetParent(m_fourCCGroups[FourCC.SCOB]);

            WDOMLayeredGroupNode tresDefLayer = new WDOMLayeredGroupNode(FourCC.TRES, MapLayer.Default, m_world);
            tresDefLayer.SetParent(m_fourCCGroups[FourCC.TRES]);

            // Now we add layers 0 to 11 for each object type.
            // Note that we do (i + 1) for the MapLayer cast in order to skip the Default enum value.
            for (int i = 0; i < 12; i++)
            {
                WDOMLayeredGroupNode actrLayer = new WDOMLayeredGroupNode(FourCCConversion.GetEnumFromString($"ACT{ i.ToString("x") }"), (MapLayer)i + 1, m_world);
                actrLayer.SetParent(m_fourCCGroups[FourCC.ACTR]);

                WDOMLayeredGroupNode scobLayer = new WDOMLayeredGroupNode(FourCCConversion.GetEnumFromString($"SCO{ i.ToString("x") }"), (MapLayer)i + 1, m_world);
                scobLayer.SetParent(m_fourCCGroups[FourCC.SCOB]);

                WDOMLayeredGroupNode tresLayer = new WDOMLayeredGroupNode(FourCCConversion.GetEnumFromString($"TRE{ i.ToString("x") }"), (MapLayer)i + 1, m_world);
                tresLayer.SetParent(m_fourCCGroups[FourCC.TRES]);
            }

            /*m_fourCCGroups["Actors"] = new WDOMGroupNode("Actors", m_world);
            WDOMGroupNode actrDefault = new WDOMGroupNode("ACTR", m_world);
            actrDefault.SetParent(m_fourCCGroups["Actors"]);

            m_fourCCGroups["Scaleable Objects"] = new WDOMGroupNode("Scaleable Objects", m_world);
            WDOMGroupNode scobDefault = new WDOMGroupNode("SCOB", m_world);
            scobDefault.SetParent(m_fourCCGroups["Scaleable Objects"]);

            m_fourCCGroups["Treasure Chests"] = new WDOMGroupNode("Treasure Chests", m_world);
            WDOMGroupNode tresDefault = new WDOMGroupNode("TRES", m_world);
            tresDefault.SetParent(m_fourCCGroups["Treasure Chests"]);

            for (int i = 0; i < 12; i++)
            {
                WDOMGroupNode actX = new WDOMGroupNode($"ACT{ i.ToString("x") }", m_world);
                actX.SetParent(m_fourCCGroups["Actors"]);

                WDOMGroupNode scoX = new WDOMGroupNode($"SCO{ i.ToString("x") }", m_world);
                scoX.SetParent(m_fourCCGroups["Scaleable Objects"]);

                WDOMGroupNode treX = new WDOMGroupNode($"TRE{ i.ToString("x") }", m_world);
                treX.SetParent(m_fourCCGroups["Treasure Chests"]);
            }*/
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

			foreach(var child in loadedActors)
			{
				var fourCCEntity = (SerializableDOMNode)child;

				if(child is Actor)
                    child.SetParent(m_fourCCGroups[FourCC.ACTR].Children[(int)fourCCEntity.Layer]);
				else if (child is ScaleableObject)
					child.SetParent(m_fourCCGroups[FourCC.SCOB].Children[(int)fourCCEntity.Layer]);
				else if (child is TreasureChest)
					child.SetParent(m_fourCCGroups[FourCC.TRES].Children[(int)fourCCEntity.Layer]);
				else
					child.SetParent(m_fourCCGroups[fourCCEntity.FourCC]);

                //m_fourCCGroups[fourCCEntity.FourCC].Children.Add(fourCCEntity);
                //child.SetParent(m_fourCCGroups[fourCCEntity.FourCC]);
				child.IsVisible = true;
			}

            List<KeyValuePair<string, FourCC>> dispFourCCs = new List<KeyValuePair<string, FourCC>>();
            foreach (var item in m_fourCCGroups)
            {
                dispFourCCs.Add(new KeyValuePair<string, FourCC>(item.Value.ToString(), item.Key));
            }

            // Sort the FourCCs alphabetically by their ToString() value
            for (int i = 0; i < dispFourCCs.Count; i++)
            {
                for (int j = i; j < dispFourCCs.Count; j++)
                {
                    if (dispFourCCs[i].Key.CompareTo(dispFourCCs[j].Key) > 0)
                    {
                        KeyValuePair<string, FourCC> temp = dispFourCCs[i];
                        dispFourCCs[i] = dispFourCCs[j];
                        dispFourCCs[j] = temp;
                    }
                }
            }

            // Add entities to the DOM in the sorted order
            foreach (KeyValuePair<string, FourCC> keyVal in dispFourCCs)
            {
                m_fourCCGroups[keyVal.Value].SetParent(this);
            }

            foreach (var child in loadedActors)
            {
                if (child is SerializableDOMNode)
                {
                    SerializableDOMNode child_as_vis = child as SerializableDOMNode;
                    child_as_vis.PostLoad();
                }
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
