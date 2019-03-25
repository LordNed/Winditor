using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor
{
    [HideCategories(new string[] { "Transform" })]
    public partial class EnvironmentLightingSkyboxPalette
    {
        public EnvironmentLightingSkyboxPalette() : base(FourCC.Virt, null)
        {

        }

        public static EnvironmentLightingSkyboxPalette Lerp(EnvironmentLightingSkyboxPalette palette_a, EnvironmentLightingSkyboxPalette palette_b, float t)
        {
            EnvironmentLightingSkyboxPalette interpSkybox = new EnvironmentLightingSkyboxPalette();
            interpSkybox.Unknown1 = WLinearColor.Lerp(palette_a.Unknown1, palette_b.Unknown1, t);
            interpSkybox.Unknown2 = WLinearColor.Lerp(palette_a.Unknown2, palette_b.Unknown2, t);
            interpSkybox.Unknown3 = WLinearColor.Lerp(palette_a.Unknown3, palette_b.Unknown3, t);
            interpSkybox.Unknown4 = WLinearColor.Lerp(palette_a.Unknown4, palette_b.Unknown4, t);

            interpSkybox.HorizonCloudColor = WLinearColor.Lerp(palette_a.HorizonCloudColor, palette_b.HorizonCloudColor, t);
            interpSkybox.CenterCloudColor = WLinearColor.Lerp(palette_a.CenterCloudColor, palette_b.CenterCloudColor, t);
            interpSkybox.SkyColor = WLinearColor.Lerp(palette_a.SkyColor, palette_b.SkyColor, t);
            interpSkybox.FalseSeaColor = WLinearColor.Lerp(palette_a.FalseSeaColor, palette_b.FalseSeaColor, t);
            interpSkybox.HorizonColor = WLinearColor.Lerp(palette_a.HorizonColor, palette_b.HorizonColor, t);

            return interpSkybox;
        }
    }
}
