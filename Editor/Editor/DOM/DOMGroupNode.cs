namespace WindEditor
{
    public class DOMGroupNode : WDOMNode
    {
		private string m_fourCC;

        public DOMGroupNode(string FourCC, WWorld world) : base(world)
        {
			m_fourCC = FourCC;
            IsVisible = true;
        }

        public override string ToString()
        {
			switch (m_fourCC)
			{
                case "Actors":
                    return "Actors";
                case "Treasure Chests":
                    return "Treasure Chests";
                case "Scaleable Objects":
                    return "Scaleable Objects";
				case "2DMA":
					return "Minimap";
				case "ACTR":
					return "Default Layer";
                case "ACT0":
                    return "Layer 0";
                case "ACT1":
                    return "Layer 1";
                case "ACT2":
                    return "Layer 2";
                case "ACT3":
                    return "Layer 3";
                case "ACT4":
                    return "Layer 4";
                case "ACT5":
                    return "Layer 5";
                case "ACT6":
                    return "Layer 6";
                case "ACT7":
                    return "Layer 7";
                case "ACT8":
                    return "Layer 8";
                case "ACT9":
                    return "Layer 9";
                case "ACTa":
                    return "Layer 10";
                case "ACTb":
                    return "Layer 11";
                case "AROB":
					return "Camera POIs";
				case "RARO":
					return "Camera POIs (Events)";
				case "CAMR":
					return "Camera Behavior (CAMR)";
				case "RCAM":
					return "Camera Behavior (RCAM)";
				case "EnvR":
					return "Environment Lighting";
				case "Colo":
					return "EnvLighting ToD Colors";
				case "Pale":
					return "EnvLighting Palette";
				case "Virt":
					return "Skybox Colors";
				case "DMAP":
					return "Dungeon Map";
				case "FLOR":
					return "Dungeon Floors";
				case "DOOR":
					return "Doors (DOOR)";
				case "TGDR":
					return "Doors (TGDR)";
				case "EVNT":
					return "Events";
				case "FILI":
					return "Room Properties";
				case "LGHT":
					return "Interior Lights";
				case "LBNK":
					return "Layer Bank";
				case "MECO":
					return "MECO";
				case "MEMA":
					return "MEMA";
				case "MULT":
					return "Room Translation";
				case "PATH":
					return "Paths (PATH)";
				case "RPAT":
					return "Paths (RPAT)";
				case "PPNT":
					return "Path Waypoints (PPNT)";
				case "RPPN":
					return "Path Waypoints (RPPN)";
				case "RTBL":
					return "Adjacent Loaded Rooms";
				case "SCLS":
					return "Exit List";
				case "SCOB":
					return "Default Layer";
                case "SCO0":
                    return "Layer 0";
                case "SCO1":
                    return "Layer 1";
                case "SCO2":
                    return "Layer 2";
                case "SCO3":
                    return "Layer 3";
                case "SCO4":
                    return "Layer 4";
                case "SCO5":
                    return "Layer 5";
                case "SCO6":
                    return "Layer 6";
                case "SCO7":
                    return "Layer 7";
                case "SCO8":
                    return "Layer 8";
                case "SCO9":
                    return "Layer 9";
                case "SCOa":
                    return "Layer 10";
                case "SCOb":
                    return "Layer 11";
                case "PLYR":
					return "Player Spawns";
				case "SHIP":
					return "Ship Spawns";
				case "SOND":
					return "Ambient Soundscape";
				case "STAG":
					return "Stage Settings";
				case "TRES":
					return "Default Layer";
                case "TRE0":
                    return "Layer 0";
                case "TRE1":
                    return "Layer 1";
                case "TRE2":
                    return "Layer 2";
                case "TRE3":
                    return "Layer 3";
                case "TRE4":
                    return "Layer 4";
                case "TRE5":
                    return "Layer 5";
                case "TRE6":
                    return "Layer 6";
                case "TRE7":
                    return "Layer 7";
                case "TRE8":
                    return "Layer 8";
                case "TRE9":
                    return "Layer 9";
                case "TREa":
                    return "Layer 10";
                case "TREb":
                    return "Layer 11";
                case "LGTV":
					return "Shadow Cast Origin";
                case "TGSC":
                    return "TGSC";
                case "TGOB":
                    return "TGOB";
				default:
					return $"Unsupported FourCC ({ m_fourCC })";
			}
        }
    }
}
