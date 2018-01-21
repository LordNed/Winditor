using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor
{
    public enum FourCC
    {
        ACTR,
        ACT0,
        ACT1,
        ACT2,
        ACT3,
        ACT4,
        ACT5,
        ACT6,
        ACT7,
        ACT8,
        ACT9,
        ACTa,
        ACTb,

        SCOB,
        SCO0,
        SCO1,
        SCO2,
        SCO3,
        SCO4,
        SCO5,
        SCO6,
        SCO7,
        SCO8,
        SCO9,
        SCOa,
        SCOb,

        TRES,
        TRE0,
        TRE1,
        TRE2,
        TRE3,
        TRE4,
        TRE5,
        TRE6,
        TRE7,
        TRE8,
        TRE9,
        TREa,
        TREb,

        MA2D,
        ma2D,
        AROB,
        RARO,
        CAMR,
        RCAM,
        EnvR,
        Colo,
        Pale,
        Virt,
        DMAP,
        FLOR,
        DOOR,
        TGDR,
        EVNT,
        FILI,
        LGHT,
        LBNK,
        MECO,
        MEMA,
        MULT,
        PATH,
        RPAT,
        PPNT,
        RPPN,
        RTBL,
        SCLS,
        PLYR,
        SHIP,
        SOND,
        STAG,
        LGTV,
        TGSC,
        TGOB,

        None
    }

    public static class FourCCConversion
    {
        public static FourCC GetEnumFromString(string fourcc)
        {
            FourCC result = FourCC.None;
            bool success = Enum.TryParse<FourCC>(fourcc, out result);

            if (success)
                return result;
            else
                throw new FormatException($"String to FourCC conversion failed for string { fourcc }!");
        }
    }
}
