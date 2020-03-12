using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class rd
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
            if (IdleAnimation == IdleAnimationEnum.Standing)
            {
                m_actorMeshes = WResourceManager.LoadActorResource("Standing ReDead");
            }
            else
            {
                m_actorMeshes = WResourceManager.LoadActorResource("Sitting ReDead");
            }
        }
	}
}
