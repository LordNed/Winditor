using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBMDLib.Materials.Enums
{
    public enum CombineColorInput
    {
        ColorPrev = 0,  // ! < Use Color Value from previous TEV stage
        AlphaPrev = 1,  // ! < Use Alpha Value from previous TEV stage
        C0 = 2,         // ! < Use the Color Value from the Color/Output Register 0
        A0 = 3,         // ! < Use the Alpha value from the Color/Output Register 0
        C1 = 4,         // ! < Use the Color Value from the Color/Output Register 1
        A1 = 5,         // ! < Use the Alpha value from the Color/Output Register 1
        C2 = 6,         // ! < Use the Color Value from the Color/Output Register 2
        A2 = 7,         // ! < Use the Alpha value from the Color/Output Register 2
        TexColor = 8,   // ! < Use the Color value from Texture
        TexAlpha = 9,   // ! < Use the Alpha value from Texture
        RasColor = 10,  // ! < Use the color value from rasterizer
        RasAlpha = 11,  // ! < Use the alpha value from rasterizer
        One = 12,
        Half = 13,
        Konst = 14,
        Zero = 15       // 
    }
}
