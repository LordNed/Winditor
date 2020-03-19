using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor
{
    public partial class obj_search
    {
        public override void PostLoad()
        {
            m_actorMeshes = WResourceManager.LoadActorResource("Searchlight");
            base.PostLoad();
        }
    }
}
