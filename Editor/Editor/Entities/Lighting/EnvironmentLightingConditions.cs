using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor
{
    [HideCategories(new string[] { "Transform" })]
    public partial class EnvironmentLightingConditions
    {
        public enum WeatherPreset
        {
            Default = 0,
            Raining = 1,
            Snowing = 2,
            Unknown2 = 3,
        }

        private EnvironmentLightingTimesOfDay m_Clear;
        private EnvironmentLightingTimesOfDay m_Raining;
        private EnvironmentLightingTimesOfDay m_Snowing;
        private EnvironmentLightingTimesOfDay m_ForestParticles;

        [WProperty("Weather Conditions", "Clear", true, "The Time-of-Day lookup for clear weather.")]
        public EnvironmentLightingTimesOfDay Clear
        {
            get { return m_Clear; }
            set
            {
                if (value != m_Clear)
                {
                    m_Clear = value;
                    OnPropertyChanged("Clear");
                }
            }
        }

        [WProperty("Weather Conditions", "Rainy", true, "The Time-of-Day lookup for rainy weather.")]
        public EnvironmentLightingTimesOfDay Raining
        {
            get { return m_Raining; }
            set
            {
                if (value != m_Raining)
                {
                    m_Raining = value;
                    OnPropertyChanged("Raining");
                }
            }
        }

        [WProperty("Weather Conditions", "Snowy", true, "The Time-of-Day lookup for snowy weather.")]
        public EnvironmentLightingTimesOfDay Snowing
        {
            get { return m_Snowing; }
            set
            {
                if (value != m_Snowing)
                {
                    m_Snowing = value;
                    OnPropertyChanged("Snowing");
                }
            }
        }

        [WProperty("Weather Conditions", "Forest Particles", true, "The Time-of-Day lookup for forest particles.")]
        public EnvironmentLightingTimesOfDay ForestParticles
        {
            get { return m_ForestParticles; }
            set
            {
                if (value != m_ForestParticles)
                {
                    m_ForestParticles = value;
                    OnPropertyChanged("ForestParticles");
                }
            }
        }

        public override void PostLoad()
        {
            if (Parent == null || Parent.Parent == null)
                return;

            List<EnvironmentLightingTimesOfDay> times = Parent.Parent.GetChildrenOfType<EnvironmentLightingTimesOfDay>();

            Clear   = m_ClearIndex < times.Count           ? times[m_ClearIndex]           : null;
            Raining = m_RainingIndex < times.Count         ? times[m_RainingIndex]         : null;
            Snowing = m_SnowingIndex < times.Count         ? times[m_SnowingIndex]         : null;
            ForestParticles = m_ForestParticlesIndex < times.Count ? times[m_ForestParticlesIndex] : null;
        }

        public override void PreSave()
        {
            if (Parent == null || Parent.Parent == null)
                return;

            List<WDOMNode> times = Parent.Parent.GetChildrenOfType(typeof(EnvironmentLightingTimesOfDay));

            m_ClearIndex   = Clear           != null ? (byte)times.IndexOf(Clear)           : (byte)0;
            m_RainingIndex = Raining         != null ? (byte)times.IndexOf(Raining)         : (byte)0;
            m_SnowingIndex = Snowing         != null ? (byte)times.IndexOf(Snowing)         : (byte)0;
            m_ForestParticlesIndex = ForestParticles != null ? (byte)times.IndexOf(ForestParticles) : (byte)0;
        }

        public EnvironmentLightingPalette Lerp(WeatherPreset weather, bool presetA, float t)
        {
            EnvironmentLightingTimesOfDay cur_time = null;

            switch (weather)
            {
                case WeatherPreset.Default:
                    cur_time = Clear;
                    break;
                case WeatherPreset.Raining:
                    cur_time = Raining;
                    break;
                case WeatherPreset.Snowing:
                    cur_time = Snowing;
                    break;
                case WeatherPreset.Unknown2:
                    cur_time = ForestParticles;
                    break;
                default:
                    return null;
            }

            return cur_time.Lerp(t);
        }
    }
}
