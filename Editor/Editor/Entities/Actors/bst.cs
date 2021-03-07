using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor
{
    public partial class bst
    {
        public override void PostLoad()
        {
            UpdateModel();
        }

        private void UpdateModel()
        {
            switch (ComponentType)
            {
                case ComponentTypeEnum.Head:
                    m_actorMeshes = WResourceManager.LoadActorResource("Gohdan Head");
                    break;
                case ComponentTypeEnum.Left_Hand:
                    m_actorMeshes = WResourceManager.LoadActorResource("Gohdan Left Hand");
                    break;
                case ComponentTypeEnum.Right_Hand:
                    m_actorMeshes = WResourceManager.LoadActorResource("Gohdan Right Hand");
                    break;
            }
        }
    }
}
