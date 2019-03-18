using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class leaflift
	{
		public override void PostLoad()
        {
            m_actorMeshes = WResourceManager.LoadActorResource("Great Deku Tree Lily Pad");
		}

		public override void PreSave()
		{

		}
	}
}
