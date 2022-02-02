using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class nz
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
            if (Type == TypeEnum.Bombchu)
            {
                m_actorMeshes = WResourceManager.LoadActorResource("Bombchu");
            }
            else
            {
                m_actorMeshes = WResourceManager.LoadActorResource("Rat");
            }
        }
	}
}
