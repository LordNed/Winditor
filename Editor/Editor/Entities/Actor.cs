using JStudio.J3D;
using OpenTK;
using System;

namespace WindEditor.a
{
	public partial class Actor : ILayerable
	{
        public MapLayer Layer { get; set; }

        public override string ToString()
		{
			return Name;
		}

        public override void PostLoad()
        {
            Layer = FourCCToLayer();
        }

        public MapLayer FourCCToLayer()
        {
            string fourcc_str = FourCC.ToString().ToLowerInvariant();

            // "ACTR" is the default layer, always loaded.
            if (fourcc_str.EndsWith("r"))
            {
                return MapLayer.Default;
            }
            // Layers 10 and 11 use hexadecimal numbers A and B; we have to handle
            // these separately from layers 0-9.
            else if (fourcc_str.EndsWith("a"))
            {
                return MapLayer.LayerA;
            }
            else if (fourcc_str.EndsWith("b"))
            {
                return MapLayer.LayerB;
            }
            else
            {
                // The FourCC is "ACT0" through "ACT9", so we can get the proper layer
                // by adding 1 to the integer at the end of the FourCC string.
                int layer_no = Convert.ToInt32(fourcc_str[fourcc_str.Length - 1]);
                return (MapLayer)(layer_no + 1);
            }
        }

        public FourCC LayerToFourCC()
        {
            // Default layer
            if (Layer == MapLayer.Default)
            {
                return FourCC.ACTR;
            }
            // Like above, layers 10 and 11 need special handling.
            else if (Layer == MapLayer.LayerA)
            {
                return FourCC.ACTa;
            }
            else if (Layer == MapLayer.LayerB)
            {
                return FourCC.ACTb;
            }
            // The FourCCs for layers 0-9 can be calculated by the layer index + 1.
            else
            {
                return (FourCC)(1 + (int)Layer);
            }
        }
    }
}
