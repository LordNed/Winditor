using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBMDLib.Materials.Mdl
{
    public enum XFRegister : int
    {
        SETNUMCHAN = 0x1009,
        SETCHAN0_AMBCOLOR = 0x100A,
        SETCHAN0_MATCOLOR = 0x100C,
        SETCHAN0_COLOR = 0x100E,
        SETNUMTEXGENS = 0x103F,
        SETTEXMTXINFO = 0x1040,
        SETPOSMTXINFO = 0x1050,
    }
}
