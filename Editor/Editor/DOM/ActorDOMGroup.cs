using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor
{
    public class ActorDOMGroup : WDOMNode
    {
        public ActorDOMGroup(WWorld world) : base(world)
        {
        }

        public override string ToString()
        {
            return "Actors";
        }
    }
}
