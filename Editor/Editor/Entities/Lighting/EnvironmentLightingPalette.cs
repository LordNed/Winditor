using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor.a
{
    [HideCategories(new string[] { "Transform" })]
    public partial class EnvironmentLightingPalette
    {
        private EnvironmentLightingSkyboxPalette m_SkyboxPalette;

        [WProperty("Skybox", "Skybox Palette", true, "The palette to use for the map's skybox.")]
        public EnvironmentLightingSkyboxPalette SkyboxPalette
        {
            get { return m_SkyboxPalette; }
            set
            {
                if (value != m_SkyboxPalette)
                {
                    m_SkyboxPalette = value;
                    OnPropertyChanged("SkyboxPalette");
                }
            }
        }

        public EnvironmentLightingPalette() : base(null, FourCC.Pale)
        {

        }

        public override void PostLoad()
        {
            if (Parent == null || Parent.Parent == null)
                return;

            List<EnvironmentLightingSkyboxPalette> skybox_palettes = Parent.Parent.GetChildrenOfType<EnvironmentLightingSkyboxPalette>();

            SkyboxPalette = m_SkyboxPaletteIndex < skybox_palettes.Count ? skybox_palettes[m_SkyboxPaletteIndex] : null;
        }

        public override void PreSave()
        {
            if (Parent == null || Parent.Parent == null)
                return;

            List<EnvironmentLightingSkyboxPalette> skybox_palettes = Parent.Parent.GetChildrenOfType<EnvironmentLightingSkyboxPalette>();

            m_SkyboxPaletteIndex = SkyboxPalette != null ? (byte)skybox_palettes.IndexOf(SkyboxPalette) : (byte)0;
        }
    }
}
