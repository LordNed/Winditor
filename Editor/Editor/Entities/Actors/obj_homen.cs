using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class obj_homen
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
            if (Type == TypeEnum.Large_Stone_Head)
            {
                m_actorMeshes = WResourceManager.LoadActorResource("Large Stone Head");
            }
            else
            {
                m_actorMeshes = WResourceManager.LoadActorResource("Small Stone Head");
            }
        }
	}
}
