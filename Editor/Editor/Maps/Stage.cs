using GameFormatReader.Common;
using System;
using System.Collections.Generic;
using System.IO;

namespace WindEditor
{
    public class WStage : WScene
    {
        private WSkyboxNode m_skybox;

        public WStage(WWorld world) : base(world)
        {
            IsRendered = true;
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
                            if (File.Exists(fileName))
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
            if (File.Exists(dzsFilePath))
            {
                SceneDataLoader sceneData = new SceneDataLoader(dzsFilePath, m_world);
                // Load Room Translation info. Wind Waker stores collision and entities in world-space coordinates,
                // but models all of their rooms around 0,0,0. To solve this, there is a chunk labeled "MULT" which stores
                // the room model's translation and rotation.
                var multTable = sceneData.GetRoomTransformTable();
                if (mapRooms.Count != multTable.Count)
                    Console.WriteLine("WStage: Mismatched number of entries in Mult Table ({0}) and number of loaded rooms ({1})!", multTable.Count, mapRooms.Count);

                for (int i = 0; i < multTable.Count; i++)
                {
                    WRoom room = mapRooms.Find(x => x.RoomIndex == multTable[i].RoomNumber);
                    if (room != null)
                        room.RoomTransform = multTable[i];
                }

                // Load Room Memory Allocation info. How much extra memory do these rooms allocate?
                var allocTable = sceneData.GetRoomMemAllocTable();
                if (mapRooms.Count != allocTable.Count)
                    Console.WriteLine("WStage: Mismatched number of entries in Meco Table ({0}) and number of loaded rooms ({1})!", allocTable.Count, mapRooms.Count);

                for (int i = 0; i < allocTable.Count; i++)
                {
                    WRoom room = mapRooms.Find(x => allocTable[i].RoomIndex == x.RoomIndex);
                    if (room != null)
                        room.MemoryAllocation = allocTable[i].MemorySize;
                }

                // Extract our EnvR data.
                var envrData = GetLightingData();
				
                // This doesn't always match up, as sea has 52 EnvR entries but only 50 rooms, but meh.
                if (mapRooms.Count != envrData.Count)
                {
                    Console.WriteLine("WStage: Mismatched number of entries in Envr ({0}) and number of loaded rooms ({1})!",
                        envrData.Count, mapRooms.Count);
                }
				
                if (envrData.Count > 0)
                {
                    foreach (var room in mapRooms)
                    {
                        room.EnvironmentLighting = envrData[0];
                    }
                }

                for (int i = 0; i < envrData.Count; i++)
                {
                    WRoom room = mapRooms.Find(x => x.RoomIndex == i);
                    if (room != null)
                        room.EnvironmentLighting = envrData[i];
                }
            }
        }

        private List<EnvironmentLighting> GetLightingData()
        {
            List<EnvironmentLighting> lights = new List<EnvironmentLighting>();
            List<LightingTimePreset> times = new List<LightingTimePreset>();
            List<LightingPalette> palettes = new List<LightingPalette>();
            List<LightingSkyboxColors> skyboxes = new List<LightingSkyboxColors>();

            if (!m_fourCCGroups.ContainsKey(FourCC.EnvR) ||
                !m_fourCCGroups.ContainsKey(FourCC.Colo) ||
                !m_fourCCGroups.ContainsKey(FourCC.Virt) ||
                !m_fourCCGroups.ContainsKey(FourCC.Pale))
                return lights;

            foreach (var virt in m_fourCCGroups[FourCC.Virt].Children)
            {
                EnvironmentLightingSkyboxColors skybox = (EnvironmentLightingSkyboxColors)virt;
                LightingSkyboxColors skycolors = new LightingSkyboxColors(skybox);

                skyboxes.Add(skycolors);
            }

            foreach (var pale in m_fourCCGroups[FourCC.Pale].Children)
            {
                EnvironmentLightingColors colors = (EnvironmentLightingColors)pale;
                LightingPalette palette = new LightingPalette(colors);

                if (colors.SkyboxColorIndex < skyboxes.Count)
                    palette.Skybox = skyboxes[colors.SkyboxColorIndex];
                else
                    palette.Skybox = new LightingSkyboxColors();

                palettes.Add(palette);
            }

            foreach (var colo in m_fourCCGroups[FourCC.Colo].Children)
            {
                EnvironmentLightingTimesOfDay daytimes = (EnvironmentLightingTimesOfDay)colo;
                LightingTimePreset preset = new LightingTimePreset();

                preset.TimePresetA[0] = palettes[daytimes.DawnA];
                preset.TimePresetA[1] = palettes[daytimes.MorningA];
                preset.TimePresetA[2] = palettes[daytimes.NoonA];
                preset.TimePresetA[3] = palettes[daytimes.AfternoonA];
                preset.TimePresetA[4] = palettes[daytimes.DuskA];
                preset.TimePresetA[5] = palettes[daytimes.NightA];

                preset.TimePresetB[0] = palettes[0];//daytimes.DawnB];
                preset.TimePresetB[1] = palettes[0]; //palettes[daytimes.MorningB];
                preset.TimePresetB[2] = palettes[0]; //palettes[daytimes.NoonB];
                preset.TimePresetB[3] = palettes[0]; //palettes[daytimes.AfternoonB];
                preset.TimePresetB[4] = palettes[0]; //palettes[daytimes.DuskB];
                preset.TimePresetB[5] = palettes[0]; //palettes[daytimes.NightB];

                times.Add(preset);
            }

            foreach (var envr in m_fourCCGroups[FourCC.EnvR].Children)
            {
                EnvironmentLightingConditions condition = (EnvironmentLightingConditions)envr;
                EnvironmentLighting env = new EnvironmentLighting();

                env.WeatherA[0] = times[condition.ClearColorA];
                env.WeatherA[1] = times[condition.RainingColorA < times.Count ? condition.RainingColorA : 0];
                env.WeatherA[2] = times[condition.SnowingA];
                env.WeatherA[3] = times[condition.UnknownA];

                env.WeatherB[0] = times[condition.ClearColorB];
                env.WeatherB[1] = times[condition.RainingColorB];
                env.WeatherB[2] = times[condition.SnowingB];
                env.WeatherB[3] = times[condition.UnknownB];

                lights.Add(env);
            }

            return lights;
        }

        public override void SetTimeOfDay(float timeOfDay)
        {
            base.SetTimeOfDay(timeOfDay);

            if (m_skybox == null)
                return;

            WRoom first_room = null;

            foreach (var node in m_world.Map.SceneList)
            {
                if (node is WRoom)
                {
                    first_room = (WRoom)node;
                    break;
                }
            }

            if (first_room == null || first_room.EnvironmentLighting == null)
                return;

            var envrData = GetLightingData();

            var curLight = envrData[0].Lerp(EnvironmentLighting.WeatherPreset.Default, true, timeOfDay);

            m_skybox.SetColors(curLight.Skybox);
        }

        public override void SaveEntitiesToDirectory(string directory)
        {
            string dzsDirectory = string.Format("{0}/dzs", directory);
            if (!Directory.Exists(dzsDirectory))
                Directory.CreateDirectory(dzsDirectory);

            string filePath = string.Format("{0}/stage.dzs", dzsDirectory);
            using (EndianBinaryWriter writer = new EndianBinaryWriter(File.Open(filePath, FileMode.Create), Endian.Big))
            {
                SceneDataExporter exporter = new SceneDataExporter();
                exporter.ExportToStream(writer, this);
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
