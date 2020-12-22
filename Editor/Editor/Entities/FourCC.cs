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

        _2DMA,
        _2Dma,
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

        NONE  // Default
    }

    public static class FourCCConversion
    {
        public static FourCC GetEnumFromString(string fourcc)
        {
            FourCC result = FourCC.NONE;

            if (fourcc == "2DMA")
                return FourCC._2DMA;
            else if (fourcc == "2Dma")
                return FourCC._2Dma;

            bool success = Enum.TryParse<FourCC>(fourcc, out result);

            if (success)
                return result;
            else
                throw new FormatException($"String to FourCC conversion failed for string { fourcc }!");
        }

        public static string GetStringFromEnum(FourCC fourcc)
        {
            if (fourcc == FourCC._2DMA)
                return "2DMA";
            else if (fourcc == FourCC._2Dma)
                return "2Dma";

            return fourcc.ToString();
        }

        public static string GetDescriptionFromEnum(FourCC fourcc)
        {
            switch (fourcc)
            {
                case FourCC._2DMA:
                    return "2DMA\tMinimap Settings";
                case FourCC._2Dma:
                    return "2Dma\tMinimap Settings (Unused)";
                case FourCC.ACTR:
                    return "Default Layer";
                case FourCC.ACT0:
                    return "Layer 0";
                case FourCC.ACT1:
                    return "Layer 1";
                case FourCC.ACT2:
                    return "Layer 2";
                case FourCC.ACT3:
                    return "Layer 3";
                case FourCC.ACT4:
                    return "Layer 4";
                case FourCC.ACT5:
                    return "Layer 5";
                case FourCC.ACT6:
                    return "Layer 6";
                case FourCC.ACT7:
                    return "Layer 7";
                case FourCC.ACT8:
                    return "Layer 8";
                case FourCC.ACT9:
                    return "Layer 9";
                case FourCC.ACTa:
                    return "Layer 10";
                case FourCC.ACTb:
                    return "Layer 11";
                case FourCC.AROB:
                    return "AROB\tCamera POIs";
                case FourCC.RARO:
                    return "RARO\tEvent Camera POIs";
                case FourCC.CAMR:
                    return "CAMR\tCamera Behavior";
                case FourCC.RCAM:
                    return "RCAM\tRefactored Camera Behavior";
                case FourCC.EnvR:
                    return "EnvR\tWeather-Based Lighting References";
                case FourCC.Colo:
                    return "Colo\tTime of Day-Based Lighting References";
                case FourCC.Pale:
                    return "Pale\tEnvironment Lighting Palette Data";
                case FourCC.Virt:
                    return "Virt\tSkybox Lighting Palette Data";
                case FourCC.DMAP:
                    return "DMAP\tDungeon Map Settings";
                case FourCC.FLOR:
                    return "FLOR\tDungeon Floor Settings";
                case FourCC.DOOR:
                    return "DOOR\tDoors (Equivalent to TGDR)";
                case FourCC.TGDR:
                    return "TGDR\tDoors (Equivalent to DOOR)";
                case FourCC.EVNT:
                    return "EVNT\tMap Event Data";
                case FourCC.FILI:
                    return "FILI\tRoom Settings";
                case FourCC.LGHT:
                    return "LGHT\tInterior Lights";
                case FourCC.LBNK:
                    return "LBNK\tLayer-Based Cutscene Loader";
                case FourCC.MECO:
                    return "MECO\tMECO";
                case FourCC.MEMA:
                    return "MEMA\tMEMA";
                case FourCC.MULT:
                    return "MULT\tRoom Mesh Orientation Data";
                case FourCC.PATH:
                    return "PATH\tPaths";
                case FourCC.RPAT:
                    return "RPAT\tRefactored Paths";
                case FourCC.PPNT:
                    return "PPNT\tPath Waypoints";
                case FourCC.RPPN:
                    return "RPPN\tRefactored Path Waypoints";
                case FourCC.RTBL:
                    return "RTBL\tAdjacent Room Loading Settings";
                case FourCC.SCLS:
                    return "SCLS\tExit List";
                case FourCC.SCOB:
                    return "Default Layer";
                case FourCC.SCO0:
                    return "Layer 0";
                case FourCC.SCO1:
                    return "Layer 1";
                case FourCC.SCO2:
                    return "Layer 2";
                case FourCC.SCO3:
                    return "Layer 3";
                case FourCC.SCO4:
                    return "Layer 4";
                case FourCC.SCO5:
                    return "Layer 5";
                case FourCC.SCO6:
                    return "Layer 6";
                case FourCC.SCO7:
                    return "Layer 7";
                case FourCC.SCO8:
                    return "Layer 8";
                case FourCC.SCO9:
                    return "Layer 9";
                case FourCC.SCOa:
                    return "Layer 10";
                case FourCC.SCOb:
                    return "Layer 11";
                case FourCC.PLYR:
                    return "PLYR\tPlayer Spawn Points";
                case FourCC.SHIP:
                    return "SHIP\tShip Spawn Points";
                case FourCC.SOND:
                    return "SOND\tAmbient Sound Settings";
                case FourCC.STAG:
                    return "STAG\tStage Settings";
                case FourCC.TRES:
                    return "Default Layer";
                case FourCC.TRE0:
                    return "Layer 0";
                case FourCC.TRE1:
                    return "Layer 1";
                case FourCC.TRE2:
                    return "Layer 2";
                case FourCC.TRE3:
                    return "Layer 3";
                case FourCC.TRE4:
                    return "Layer 4";
                case FourCC.TRE5:
                    return "Layer 5";
                case FourCC.TRE6:
                    return "Layer 6";
                case FourCC.TRE7:
                    return "Layer 7";
                case FourCC.TRE8:
                    return "Layer 8";
                case FourCC.TRE9:
                    return "Layer 9";
                case FourCC.TREa:
                    return "Layer 10";
                case FourCC.TREb:
                    return "Layer 11";
                case FourCC.LGTV:
                    return "LGTV\tShadow Cast Origin";
                case FourCC.TGSC:
                    return "TGSC\tScaleable Objects (Equivalent to SCOB)";
                case FourCC.TGOB:
                    return "TGOB\tActors (Equivalent to ACTR)";
                default:
                    return $"Unsupported FourCC ({ fourcc })";
            }
        }

        public static Type GetTypeFromEnum(FourCC value)
        {
            MapActorDescriptor template = Globals.ActorDescriptors.Find(x => x.FourCC == value);
            return Type.GetType($"WindEditor.{template.ClassName}");
        }
    }
}
