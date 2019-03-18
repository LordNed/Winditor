using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor
{
    public partial class npc_zl1
    {
        public override void PostLoad()
        {
            m_actorMeshes = WResourceManager.LoadActorResource("Tetra");
        }
    }
}
