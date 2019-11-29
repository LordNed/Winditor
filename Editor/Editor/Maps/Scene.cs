using GameFormatReader.Common;
using System.Collections.Generic;
using System.IO;
using WindEditor.Collision;
using JStudio.J3D;
using System;
using WArchiveTools.FileSystem;

namespace WindEditor.a
{
    public abstract class WScene : WDOMNode
    {
        override public string Name { get; set; }
        public VirtualFilesystemDirectory SourceDirectory { get; set; }
        public EnvironmentLightingConditions EnvironmentLighting { get; set; }

        protected Dictionary<FourCC, WDOMOrganizerNode> m_fourCCGroups;

        public WScene(WWorld world) : base(world)
        {
            m_fourCCGroups = new Dictionary<FourCC, WDOMOrganizerNode>();

            // We're going to iterate through the enum values to create DOM nodes for them.
            // We're skipping all of the actors, scaleable objects, and treasure chests though, because they're special.
            foreach (FourCC f in Enum.GetValues(typeof(FourCC)))
            {
                // Skip Actors/Scaleable Objects/Treasure Chests
                if (f.ToString().Contains("ACT") || f.ToString().Contains("SCO") || f.ToString().Contains("TRE") || f == FourCC.NONE)
                {
                    continue;
                }

                m_fourCCGroups[f] = new WDOMOrganizerNode(World, FourCCConversion.GetTypeFromEnum(f), FourCCConversion.GetDescriptionFromEnum(f));
            }

            // To handle the fact that actors/scaleable/treasure chests have layers, we're going to create DOM nodes using
            // the default layer's FourCC (ACTR/SCOB/TRES). This DOM node won't interact directly with the entities, rather
            // it will be the parent node of the nodes that do. WDOMGroupNode.ToString() is overridden to return a more general
            // description of them ("Actors", etc) instead of the FourCC's FourCCConversion.GetDescriptionFromEnum() value.
            m_fourCCGroups[FourCC.ACTR] = new WDOMOrganizerNode(World, typeof(WDOMOrganizerNode), FourCCConversion.GetDescriptionFromEnum(FourCC.ACTR));
            m_fourCCGroups[FourCC.SCOB] = new WDOMOrganizerNode(World, typeof(WDOMOrganizerNode), FourCCConversion.GetDescriptionFromEnum(FourCC.SCOB));
            m_fourCCGroups[FourCC.TRES] = new WDOMOrganizerNode(World, typeof(WDOMOrganizerNode), FourCCConversion.GetDescriptionFromEnum(FourCC.TRES));

            // Now we add the default layer for each object type. WDOMLayeredGroupNode directly interacts with the entities.
            WDOMOrganizerNode actrDefLayer = new WDOMOrganizerNode(World, typeof(Actor), "Default Layer");
            actrDefLayer.SetParent(m_fourCCGroups[FourCC.ACTR]);

            WDOMOrganizerNode scobDefLayer = new WDOMOrganizerNode(World, typeof(ScaleableObject), "Default Layer");
            scobDefLayer.SetParent(m_fourCCGroups[FourCC.SCOB]);

            WDOMOrganizerNode tresDefLayer = new WDOMOrganizerNode(World, typeof(TreasureChest), "Default Layer");
            tresDefLayer.SetParent(m_fourCCGroups[FourCC.TRES]);

            // Now we add layers 0 to 11 for each object type.
            // Note that we do (i + 1) for the MapLayer cast in order to skip the Default enum value.
            for (int i = 0; i < 12; i++)
            {
                WDOMOrganizerNode actrLayer = new WDOMOrganizerNode(World, typeof(Actor), $"Layer { i }");
                actrLayer.SetParent(m_fourCCGroups[FourCC.ACTR]);

                WDOMOrganizerNode scobLayer = new WDOMOrganizerNode(World, typeof(ScaleableObject), $"Layer { i }");
                scobLayer.SetParent(m_fourCCGroups[FourCC.SCOB]);

                WDOMOrganizerNode tresLayer = new WDOMOrganizerNode(World, typeof(TreasureChest), $"Layer { i }");
                tresLayer.SetParent(m_fourCCGroups[FourCC.TRES]);
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

            WDOMOrganizerNode col_category = new WDOMOrganizerNode(World, typeof(WCollisionMesh), "Collision");
            col_category.SetParent(this);

            //WCollisionMesh collision = new WCollisionMesh(m_world, filePath);
            //collision.SetParent(col_category);
        }

        protected virtual void LoadLevelEntitiesFromFile(string filePath)
        {
            SceneDataLoader actorLoader = new SceneDataLoader(filePath, World);

            Console.WriteLine(Path.GetFileName(filePath));
            List<WDOMEntityNode> loadedActors = actorLoader.GetMapEntities();

			foreach(var child in loadedActors)
			{
                if (child is ILayerable)
                {
                    var layerable = child as ILayerable;

                    if (child.FourCC >= FourCC.ACTR && child.FourCC <= FourCC.ACTb)
                        child.SetParent(m_fourCCGroups[FourCC.ACTR].Children[(int)layerable.Layer]);

                    else if (child.FourCC >= FourCC.SCOB && child.FourCC <= FourCC.SCOb)
                        child.SetParent(m_fourCCGroups[FourCC.SCOB].Children[(int)layerable.Layer]);

                    else if (child.FourCC >= FourCC.TRES && child.FourCC <= FourCC.TREb)
                        child.SetParent(m_fourCCGroups[FourCC.TRES].Children[(int)layerable.Layer]);
                }
                else
                {
                    child.SetParent(m_fourCCGroups[child.FourCC]);
                }

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

            foreach (WDOMEntityNode child in loadedActors)
            {
                child.PostLoad();
            }
        }

        public virtual void SetTimeOfDay(float time)
        {
            if (EnvironmentLighting == null)
            {
                return;
            }

            var curLight = EnvironmentLighting.Lerp(EnvironmentLightingConditions.WeatherPreset.Default, true, time);

            List<WDOMJ3DRenderNode> j3d_nodes = GetChildrenOfType<WDOMJ3DRenderNode>();

            foreach (var node in j3d_nodes)
            {
                node.Renderable.ApplyEnvironmentLighting(curLight);
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

        public virtual VirtualFilesystemDirectory ExportToVFS()
        {
            return null;
        }
    }
}
