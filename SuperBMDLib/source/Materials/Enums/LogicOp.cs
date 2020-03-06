using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBMDLib.Materials.Enums
{
    public enum LogicOp
    {
        Clear = 0,
        And = 1,
        Copy = 3,
        Equiv = 9,
        Inv = 10,
        InvAnd = 4,
        InvCopy = 12,
        InvOr = 13,
        NAnd = 14,
        NoOp = 5,
        NOr = 8,
        Or = 7,
        RevAnd = 2,
        RevOr = 11,
        Set = 15,
        XOr = 6
    }
}
