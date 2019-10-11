using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class obj_movebox
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
            switch(Unknown_4)
            {
                case 6:
                    m_actorMeshes = WResourceManager.LoadActorResource("Pushable Metal Box");
                    break;
                case 7:
                    m_actorMeshes = WResourceManager.LoadActorResource("Pushable Metal Box With Spring");
                    break;
            }
        }
	}
}
