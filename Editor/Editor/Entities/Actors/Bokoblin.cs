using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor
{
    public partial class bk
    {
        public override void PostLoad()
        {
            UpdateModel();
        }

        private void UpdateModel()
        {
            m_actorMeshes = WResourceManager.LoadActorResource("Blue Bokoblin");

            if (IsGreen)
            {
                m_actorMeshes = WResourceManager.LoadActorResource("Green Bokoblin");
            }
            
            if (Type == TypeEnum.Pink_Bokoblin_with_Telescope)
            {
                m_actorMeshes = WResourceManager.LoadActorResource("Pink Bokoblin");
            }
        }
    }
}
