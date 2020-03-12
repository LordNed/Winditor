using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class pw
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
            // NOTE: BTP animations are not implemented currently, so this code won't actually change the color the Poe appears as yet.
            switch (Color)
            {
                case ColorEnum.Blue:
                    m_actorMeshes = WResourceManager.LoadActorResource("Blue Poe");
                    break;
                case ColorEnum.Purple:
                    m_actorMeshes = WResourceManager.LoadActorResource("Purple Poe");
                    break;
                case ColorEnum.Orange:
                    m_actorMeshes = WResourceManager.LoadActorResource("Orange Poe");
                    break;
                case ColorEnum.Yellow:
                    m_actorMeshes = WResourceManager.LoadActorResource("Yellow Poe");
                    break;
                case ColorEnum.Red:
                    m_actorMeshes = WResourceManager.LoadActorResource("Red Poe");
                    break;
                case ColorEnum.Green:
                    m_actorMeshes = WResourceManager.LoadActorResource("Green Poe");
                    break;
                default:
                    m_actorMeshes = WResourceManager.LoadActorResource("Blue Poe");
                    break;
            }
        }
	}
}
