using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBMDLib.Scenegraph.Enums
{
    public enum NodeType
    {
        Terminator = 0,
        OpenChild = 1,
        CloseChild = 2,
        Joint = 16,
        Material = 17,
        Shape = 18
    }
}
