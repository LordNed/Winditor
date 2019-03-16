using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor
{
    public partial class Medli
    {
        public override void PostLoad()
        {
            GetPropertiesFromParameters();

            m_actorMeshes = WResourceManager.LoadActorResource("Medli");
        }
    }
}
