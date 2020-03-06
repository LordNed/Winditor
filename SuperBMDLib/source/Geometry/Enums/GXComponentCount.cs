using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBMDLib.Geometry.Enums
{
    public enum GXComponentCount
    {
        Position_XY = 0,
        Position_XYZ,

        Normal_XYZ = 0,
        Normal_NBT,
        Normal_NBT3,

        Color_RGB = 0,
        Color_RGBA,

        TexCoord_S = 0,
        TexCoord_ST
    }
}
