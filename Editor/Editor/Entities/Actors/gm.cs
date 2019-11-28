using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class gm
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
            if (Unknown_3 == 1)
            {
                m_actorMeshes = WResourceManager.LoadActorResource("Wingless Mothula");
            }
            else
            {
                m_actorMeshes = WResourceManager.LoadActorResource("Winged Mothula");
            }
        }
	}
}
