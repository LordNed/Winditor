using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class obj_try
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
            switch (Type)
            {
                case TypeEnum.Unknown_6:
                    m_actorMeshes = WResourceManager.LoadActorResource("Red Tower of the Gods Pillar Statue");
                    break;
                default:
                    m_actorMeshes = WResourceManager.LoadActorResource("Blue Tower of the Gods Pillar Statue");
                    break;
            }
        }
	}
}
