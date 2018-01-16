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
			switch (m_fourCC.ToUpperInvariant())
			{
				case "2DMA":
					return "Minimap";
				case "ACTR":
					return "Actors";
				case "AROB":
					return "Camera POIs";
				case "RARO":
					return "Camera POIs (Events)";
				case "CAMR":
					return "Camera Behavior (CAMR)";
				case "RCAM":
					return "Camera Behavior (RCAM)";
				case "ENVR":
					return "Environment Lighting";
				case "COLO":
					return "EnvLighting ToD Colors";
				case "PALE":
					return "EnvLighting Palette";
				case "VIRT":
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
					return "Scaleable Actors";
				case "PLYR":
					return "Player Spawns";
				case "SHIP":
					return "Ship Spawns";
				case "SOND":
					return "Ambient Soundscape";
				case "STAG":
					return "Stage Settings";
				case "TRES":
					return "Treasure Chests";
				case "LGTV":
					return "Shadow Cast Origin";
				default:
					return $"Unsupported FourCC ({m_fourCC})";
			}
        }
    }
}
