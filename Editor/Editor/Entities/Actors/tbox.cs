using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
    public partial class tbox
    {
        public override void PostLoad()
        {
            UpdateModel();
            base.PostLoad();
        }

        public override void PreSave()
        {

        }

        private void UpdateModel()
        {
            switch (AppearanceType)
            {
                case AppearanceTypeEnum.Big_Key:
                    m_actorMeshes = WResourceManager.LoadActorResource("Big Key Chest");
                    break;
                case AppearanceTypeEnum.Metal:
                    m_actorMeshes = WResourceManager.LoadActorResource("Metal Chest");
                    break;
                case AppearanceTypeEnum.Dark_wood:
                    m_actorMeshes = WResourceManager.LoadActorResource("Dark Wood Chest");
                    break;
                case AppearanceTypeEnum.Light_wood:
                default:
                    m_actorMeshes = WResourceManager.LoadActorResource("Light Wood Chest");
                    break;
            }
        }
    }
}
