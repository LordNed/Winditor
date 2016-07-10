using System;

namespace WindEditor
{
    /// <summary>
    /// Represents the "Colo" chunk in map data which has palettes for the different times of day.
    /// Dawn, Morning, Noon, Afternoon, Dusk, Night
    /// There are two copies of this data, A and B. B's usage is unknown.
    /// </summary>
    public class LightingTimePreset
    {
        public LightingPalette[] TimePresetA;
        public LightingPalette[] TimePresetB;

        public LightingTimePreset()
        {
            TimePresetA = new LightingPalette[6];
            TimePresetB = new LightingPalette[6];
        }

        public LightingPalette Lerp(float t, bool presetA = true)
        {
            // Generate a new LightingPalette which is the interpolated values of things.
            t = WMath.Clamp(t, 0, 1);
            int lowerIndex = (int)Math.Floor(t * 6f);
            int upperIndex = (int)Math.Floor(t * 6f) + 1;

            LightingPalette[] palette = presetA ? TimePresetA : TimePresetB;
            LightingPalette interpPalette = new LightingPalette();
            interpPalette.Shadow = WLinearColor.Lerp(palette[lowerIndex].Shadow, palette[upperIndex].Shadow, t);
            interpPalette.ActorAmbient = WLinearColor.Lerp(palette[lowerIndex].ActorAmbient, palette[upperIndex].ActorAmbient, t);
            interpPalette.RoomLight = WLinearColor.Lerp(palette[lowerIndex].RoomLight, palette[upperIndex].RoomLight, t);
            interpPalette.RoomAmbient = WLinearColor.Lerp(palette[lowerIndex].RoomAmbient, palette[upperIndex].RoomAmbient, t);
            interpPalette.WaveColor = WLinearColor.Lerp(palette[lowerIndex].WaveColor, palette[upperIndex].WaveColor, t);
            interpPalette.OceanColor = WLinearColor.Lerp(palette[lowerIndex].OceanColor, palette[upperIndex].OceanColor, t);
            interpPalette.UnknownWhite1 = WLinearColor.Lerp(palette[lowerIndex].UnknownWhite1, palette[upperIndex].UnknownWhite1, t);
            interpPalette.UnknownWhite2 = WLinearColor.Lerp(palette[lowerIndex].UnknownWhite2, palette[upperIndex].UnknownWhite2, t);
            interpPalette.Doorway = WLinearColor.Lerp(palette[lowerIndex].Doorway, palette[upperIndex].Doorway, t);
            interpPalette.UnknownColor3 = WLinearColor.Lerp(palette[lowerIndex].UnknownColor3, palette[upperIndex].UnknownColor3, t);
            interpPalette.Skybox = LightingSkyboxColors.Lerp(palette[lowerIndex].Skybox, palette[upperIndex].Skybox, t);
            interpPalette.Fog = WLinearColor.Lerp(palette[lowerIndex].Fog, palette[upperIndex].Fog, t);
            interpPalette.FogNearPlane = WMath.Lerp(palette[lowerIndex].FogNearPlane, palette[upperIndex].FogNearPlane, t);
            interpPalette.FogFarPlane = WMath.Lerp(palette[lowerIndex].FogFarPlane, palette[upperIndex].FogFarPlane, t);

            return interpPalette;
        }
    }

    /// <summary>
    /// Represents the "Pale" chunk in map data which is a particular palette of colors for
    /// the various things in the world which need lighting.
    /// </summary>
    public class LightingPalette
    {
        public WLinearColor Shadow { get; set; }
        public WLinearColor ActorAmbient { get; set; }
        public WLinearColor RoomLight { get; set; }
        public WLinearColor RoomAmbient { get; set; }
        public WLinearColor WaveColor { get; set; }
        public WLinearColor OceanColor { get; set; }
        public WLinearColor UnknownWhite1 { get; set; }
        public WLinearColor UnknownWhite2 { get; set; }
        public WLinearColor Doorway { get; set; }
        public WLinearColor UnknownColor3 { get; set; }

        public LightingSkyboxColors Skybox { get; set; }

        public WLinearColor Fog { get; set; }
        public float FogNearPlane { get; set; }
        public float FogFarPlane { get; set; }
    }

    /// <summary>
    /// Represents the "Virt" chunk in map data which is the palette for the skybox.
    /// </summary>
    public class LightingSkyboxColors
    {
        public WLinearColor Unknown1 { get; set; }
        public WLinearColor Unknown2 { get; set; }
        public WLinearColor Unknown3 { get; set; }
        public WLinearColor Unknown4 { get; set; }

        public WLinearColor HorizonCloud { get; set; }
        public WLinearColor CenterCloud { get; set; }
        public WLinearColor Sky { get; set; }
        public WLinearColor FalseSea { get; set; }
        public WLinearColor Horizon { get; set; }

        public static LightingSkyboxColors Lerp(LightingSkyboxColors a, LightingSkyboxColors b, float t)
        {
            LightingSkyboxColors interpSkybox = new LightingSkyboxColors();
            interpSkybox.Unknown1 = WLinearColor.Lerp(a.Unknown1, b.Unknown1, t);
            interpSkybox.Unknown2 = WLinearColor.Lerp(a.Unknown2, b.Unknown2, t);
            interpSkybox.Unknown3 = WLinearColor.Lerp(a.Unknown3, b.Unknown3, t);
            interpSkybox.Unknown4 = WLinearColor.Lerp(a.Unknown4, b.Unknown4, t);

            interpSkybox.HorizonCloud = WLinearColor.Lerp(a.HorizonCloud, b.HorizonCloud, t);
            interpSkybox.CenterCloud = WLinearColor.Lerp(a.CenterCloud, b.CenterCloud, t);
            interpSkybox.Sky = WLinearColor.Lerp(a.Sky, b.Sky, t);
            interpSkybox.FalseSea = WLinearColor.Lerp(a.FalseSea, b.FalseSea, t);
            interpSkybox.Horizon = WLinearColor.Lerp(a.Horizon, b.Horizon, t);

            return interpSkybox;
        }
    }

    /// <summary>
    /// Represents the "EnvR" chunk in map data which contains lighting data for various environmental conditions.
    /// </summary>
    public class EnvironmentLighting
    {
        public enum WeatherPreset
        {
            Default = 0,
            Raining = 1,
            Snowing = 2,
            Unknown2 = 3,
        }

        public LightingTimePreset[] WeatherA { get; set; }
        public LightingTimePreset[] WeatherB { get; set; }

        public EnvironmentLighting()
        {
            WeatherA = new LightingTimePreset[4];
            WeatherB = new LightingTimePreset[4];
        }

        public LightingPalette Lerp(WeatherPreset weather, bool presetA, float t)
        {
            LightingTimePreset[] timePreset = presetA ? WeatherA : WeatherB;
            return timePreset[(int)weather].Lerp(t, presetA);
        }
    }
}
