using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using OpenTK;

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
            VisualScaleMultiplier = new Vector3(1f, 1f, 1f);
            switch (Type)
            {
                case TypeEnum.Small_Jet:
                    m_actorMeshes = WResourceManager.LoadActorResource("Medium Magma Jet");
                    VisualScaleMultiplier = new Vector3(0.5f, 0.5f, 0.5f);
                    break;
                case TypeEnum.Medium_Jet:
                default:
                    m_actorMeshes = WResourceManager.LoadActorResource("Medium Magma Jet");
                    break;
                case TypeEnum.Large_Jet:
                    m_actorMeshes = WResourceManager.LoadActorResource("Large Magma Jet");
                    VisualScaleMultiplier = new Vector3(1f, 0.815f, 1f);
                    break;
                case TypeEnum.Very_Large_Jet:
                    m_actorMeshes = WResourceManager.LoadActorResource("Large Magma Jet");
                    break;
            }
        }
	}
}
