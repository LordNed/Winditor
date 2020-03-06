using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBMDLib.Materials.Enums
{
    public enum CombineAlphaInput
    {
        AlphaPrev = 0,  // Use the Alpha value form the previous TEV stage
        A0 = 1,         // Use the Alpha value from the Color/Output Register 0
        A1 = 2,         // Use the Alpha value from the Color/Output Register 1
        A2 = 3,         // Use the Alpha value from the Color/Output Register 2
        TexAlpha = 4,   // Use the Alpha value from the Texture
        RasAlpha = 5,   // Use the Alpha value from the rasterizer
        Konst = 6,
        Zero = 7
    }
}
