using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor
{
    public interface ILayerable
    {
        MapLayer Layer { get; set; }

        MapLayer FourCCToLayer();
        FourCC LayerToFourCC();
    }
}
