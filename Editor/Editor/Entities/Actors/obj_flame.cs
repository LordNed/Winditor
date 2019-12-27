using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class obj_flame
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
            switch (Unknown_5)
            {
                case 0:
                case 3:
                default:
                    m_actorMeshes = WResourceManager.LoadActorResource("Small Pillar of Flames");
                    break;
                case 1:
                case 2:
                    m_actorMeshes = WResourceManager.LoadActorResource("Large Pillar of Flames");
                    break;
            }
        }
	}
}
