using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor
{
    [HideCategories(new string[] { "Transform" })]
    public partial class EnvironmentLightingTimesOfDay
    {
        public enum TimeOfDay
        {
            Dawn,
            Morning,
            Noon,
            Afternoon,
            Dusk,
            Night
        }

        private EnvironmentLightingPalette m_Dawn;
        private EnvironmentLightingPalette m_Morning;
        private EnvironmentLightingPalette m_Noon;
        private EnvironmentLightingPalette m_Afternoon;
        private EnvironmentLightingPalette m_Dusk;
        private EnvironmentLightingPalette m_Night;

        [WProperty("Times of Day", "Dawn", true, "The color palette to use for dawn.")]
        public EnvironmentLightingPalette Dawn
        {
            get { return m_Dawn; }
            set
            {
                if (value != m_Dawn)
                {
                    m_Dawn = value;
                    OnPropertyChanged("Dawn");
                }
            }
        }

        [WProperty("Times of Day", "Morning", true, "The color palette to use for morning.")]
        public EnvironmentLightingPalette Morning
        {
            get { return m_Morning; }
            set
            {
                if (value != m_Morning)
                {
                    m_Morning = value;
                    OnPropertyChanged("Morning");
                }
            }
        }

        [WProperty("Times of Day", "Noon", true, "The color palette to use for noon.")]
        public EnvironmentLightingPalette Noon
        {
            get { return m_Noon; }
            set
            {
                if (value != m_Noon)
                {
                    m_Noon = value;
                    OnPropertyChanged("Noon");
                }
            }
        }

        [WProperty("Times of Day", "Afternoon", true, "The color palette to use for afternoon.")]
        public EnvironmentLightingPalette Afternoon
        {
            get { return m_Afternoon; }
            set
            {
                if (value != m_Afternoon)
                {
                    m_Afternoon = value;
                    OnPropertyChanged("Afternoon");
                }
            }
        }

        [WProperty("Times of Day", "Dusk", true, "The color palette to use for dusk.")]
        public EnvironmentLightingPalette Dusk
        {
            get { return m_Dusk; }
            set
            {
                if (value != m_Dusk)
                {
                    m_Dusk = value;
                    OnPropertyChanged("Dusk");
                }
            }
        }

        [WProperty("Times of Day", "Night", true, "The color palette to use for night.")]
        public EnvironmentLightingPalette Night
        {
            get { return m_Night; }
            set
            {
                if (value != m_Night)
                {
                    m_Night = value;
                    OnPropertyChanged("Night");
                }
            }
        }

        public override void PostLoad()
        {
            if (Parent == null || Parent.Parent == null)
                return;

            List<EnvironmentLightingPalette> palettes = Parent.Parent.GetChildrenOfType<EnvironmentLightingPalette>();

            Dawn      = m_DawnIndex < palettes.Count      ? palettes[m_DawnIndex]      : null;
            Morning   = m_MorningIndex < palettes.Count   ? palettes[m_MorningIndex]   : null;
            Noon      = m_NoonIndex < palettes.Count      ? palettes[m_NoonIndex]      : null;
            Afternoon = m_AfternoonIndex < palettes.Count ? palettes[m_AfternoonIndex] : null;
            Dusk      = m_DuskIndex < palettes.Count      ? palettes[m_DuskIndex]      : null;
            Night     = m_NightIndex < palettes.Count     ? palettes[m_NightIndex]     : null;
        }

        public override void PreSave()
        {
            if (Parent == null || Parent.Parent == null)
                return;

            List<EnvironmentLightingPalette> palettes = Parent.Parent.GetChildrenOfType<EnvironmentLightingPalette>();

            m_DawnIndex      = Dawn      != null ? (byte)palettes.IndexOf(Dawn)      : (byte)0;
            m_MorningIndex   = Morning   != null ? (byte)palettes.IndexOf(Morning)   : (byte)0;
            m_NoonIndex      = Noon      != null ? (byte)palettes.IndexOf(Noon)      : (byte)0;
            m_AfternoonIndex = Afternoon != null ? (byte)palettes.IndexOf(Afternoon) : (byte)0;
            m_DuskIndex      = Dusk      != null ? (byte)palettes.IndexOf(Dusk)      : (byte)0;
            m_NightIndex     = Night     != null ? (byte)palettes.IndexOf(Night)     : (byte)0;
        }

        public EnvironmentLightingPalette Lerp(float t, bool presetA = true)
        {
            // Generate a new LightingPalette which is the interpolated values of things.
            t = WMath.Clamp(t, 0, 1);
            float scaledT = t * (6 - 1);
            int lowerIndex = (int)scaledT;
            int upperIndex = (int)(scaledT + 1f);
            float newT = scaledT - (int)scaledT;

            EnvironmentLightingPalette palette_a = null;
            EnvironmentLightingPalette palette_b = null;

            if (upperIndex == 6)
                upperIndex = lowerIndex;

            switch ((TimeOfDay)lowerIndex)
            {
                case TimeOfDay.Dawn:
                    palette_a = Dawn;
                    break;
                case TimeOfDay.Morning:
                    palette_a = Morning;
                    break;
                case TimeOfDay.Noon:
                    palette_a = Noon;
                    break;
                case TimeOfDay.Afternoon:
                    palette_a = Afternoon;
                    break;
                case TimeOfDay.Dusk:
                    palette_a = Dusk;
                    break;
                case TimeOfDay.Night:
                    palette_a = Night;
                    break;
            }

            switch ((TimeOfDay)upperIndex)
            {
                case TimeOfDay.Dawn:
                    palette_b = Dawn;
                    break;
                case TimeOfDay.Morning:
                    palette_b = Morning;
                    break;
                case TimeOfDay.Noon:
                    palette_b = Noon;
                    break;
                case TimeOfDay.Afternoon:
                    palette_b = Afternoon;
                    break;
                case TimeOfDay.Dusk:
                    palette_b = Dusk;
                    break;
                case TimeOfDay.Night:
                    palette_b = Night;
                    break;
            }

            //Console.WriteLine("t: {0} scaledT: {1} lIndex: {2} uIndex: {3} newT: {4}", t, scaledT, lowerIndex, upperIndex, newT);

            EnvironmentLightingPalette interpPalette = new EnvironmentLightingPalette();
            interpPalette.ShadowColor = WLinearColor.Lerp(palette_a.ShadowColor, palette_b.ShadowColor, newT);
            interpPalette.ActorAmbientColor = WLinearColor.Lerp(palette_a.ActorAmbientColor, palette_b.ActorAmbientColor, newT);
            interpPalette.RoomLightColor = WLinearColor.Lerp(palette_a.RoomLightColor, palette_b.RoomLightColor, newT);
            interpPalette.RoomAmbientColor = WLinearColor.Lerp(palette_a.RoomAmbientColor, palette_b.RoomAmbientColor, newT);
            interpPalette.WaveColor = WLinearColor.Lerp(palette_a.WaveColor, palette_b.WaveColor, newT);
            interpPalette.OceanColor = WLinearColor.Lerp(palette_a.OceanColor, palette_b.OceanColor, newT);
            interpPalette.UnknownWhite1 = WLinearColor.Lerp(palette_a.UnknownWhite1, palette_b.UnknownWhite1, newT);
            interpPalette.UnknownWhite2 = WLinearColor.Lerp(palette_a.UnknownWhite2, palette_b.UnknownWhite2, newT);
            interpPalette.DoorBackfill = WLinearColor.Lerp(palette_a.DoorBackfill, palette_b.DoorBackfill, newT);
            interpPalette.Unknown3 = WLinearColor.Lerp(palette_a.Unknown3, palette_b.Unknown3, newT);
            interpPalette.SkyboxPalette = EnvironmentLightingSkyboxPalette.Lerp(palette_a.SkyboxPalette, palette_b.SkyboxPalette, newT);
            interpPalette.FogColor = WLinearColor.Lerp(palette_a.FogColor, palette_b.FogColor, newT);
            interpPalette.FogNearPlane = WMath.Lerp(palette_a.FogNearPlane, palette_b.FogNearPlane, newT);
            interpPalette.FogFarPlane = WMath.Lerp(palette_a.FogFarPlane, palette_b.FogFarPlane, newT);

            return interpPalette;
        }
    }
}
