using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class windmill
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
            if (Unknown_1 == 0)
            {
                m_actorMeshes = WResourceManager.LoadActorResource("Windmill 1");
            }
            else
            {
                m_actorMeshes = WResourceManager.LoadActorResource("Windmill 2");
            }
        }
	}
}
