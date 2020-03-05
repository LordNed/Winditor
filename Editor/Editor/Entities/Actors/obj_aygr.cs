using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class obj_aygr
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
            if (HasLadder)
            {
                m_actorMeshes = WResourceManager.LoadActorResource("Lookout Platform With Ladder");
            } else
            {
                m_actorMeshes = WResourceManager.LoadActorResource("Lookout Platform Without Ladder");
            }
        }
	}
}
