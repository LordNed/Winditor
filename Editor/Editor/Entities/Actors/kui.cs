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
            switch (m_Parameters & 0x0000000F)
            {
                case 2:
                case 4:
                    m_actorMeshes = WResourceManager.LoadActorResource("Grapple Device");
                    break;
                case 3:
                    m_actorMeshes = WResourceManager.LoadActorResource("Tower of the Gods Bell");
                    break;
                default:
                    m_actorMeshes = WResourceManager.LoadActorResource("Grapple Point");
                    break;
            }
        }
	}
}
