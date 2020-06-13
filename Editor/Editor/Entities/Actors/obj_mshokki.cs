using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class obj_mshokki
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
                case TypeEnum.Pitcher:
                default:
                    m_actorMeshes = WResourceManager.LoadActorResource("Breakable Jug");
                    break;
                case TypeEnum.Plate:
                    m_actorMeshes = WResourceManager.LoadActorResource("Breakable Plate");
                    break;
                case TypeEnum.Cup:
                    m_actorMeshes = WResourceManager.LoadActorResource("Breakable Cup");
                    break;
            }
        }
	}
}
