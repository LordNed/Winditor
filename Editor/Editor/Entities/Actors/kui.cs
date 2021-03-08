using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class kui
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
            switch (ModelType)
            {
                case ModelTypeEnum.Dragon_Roost_Cavern_Grapple_Switch:
                case ModelTypeEnum.Cabana_Grapple_Switch:
                    m_actorMeshes = WResourceManager.LoadActorResource("Grapple Device");
                    break;
                case ModelTypeEnum.Tower_of_the_Gods_Bell:
                    m_actorMeshes = WResourceManager.LoadActorResource("Tower of the Gods Bell");
                    break;
                default:
                    m_actorMeshes = WResourceManager.LoadActorResource("Grapple Point");
                    break;
            }
        }
	}
}
