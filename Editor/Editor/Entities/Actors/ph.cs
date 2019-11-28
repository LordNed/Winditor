using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class ph
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
            if (Type == TypeEnum.Seahat)
            {
                m_actorMeshes = WResourceManager.LoadActorResource("Seahat");
            } else
            {
                m_actorMeshes = WResourceManager.LoadActorResource("Peahat");
            }
        }
	}
}
