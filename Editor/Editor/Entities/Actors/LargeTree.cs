using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor
{
    public partial class LargeTree
    {
        public override void PostLoad()
        {
            m_actorMeshes = WResourceManager.LoadActorResource("Large Tree");
        }
    }
}
