using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBMDLib.Materials.Enums
{
    public enum TevOp
    {
        Add = 0,
        Sub = 1,
        Comp_R8_GT = 8,
        Comp_R8_EQ = 9,
        Comp_GR16_GT = 10,
        Comp_GR16_EQ = 11,
        Comp_BGR24_GT = 12,
        Comp_BGR24_EQ = 13,
        Comp_RGB8_GT = 14,
        Comp_RGB8_EQ = 15,
        Comp_A8_EQ = Comp_RGB8_EQ,
        Comp_A8_GT = Comp_RGB8_GT
    }
}
