using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class mo2
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
            if (Type == TypeEnum.Brown_skin_with_lantern)
            {
                m_actorMeshes = WResourceManager.LoadActorResource("Moblin");
            }
            else
            {
                m_actorMeshes = WResourceManager.LoadActorResource("Blue Moblin");
            }
        }
	}
}
