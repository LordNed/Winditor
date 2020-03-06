using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBMDLib.Geometry.Enums
{
    public enum GXDataType
    {
        Unsigned8, RGB565  = 0x0,
        Signed8,   RGB8    = 0x1,
        Unsigned16,RGBX8   = 0x2,
        Signed16,  RGBA4   = 0x3,
        Float32,   RGBA6   = 0x4,
        RGBA8              = 0x5
    }
}
