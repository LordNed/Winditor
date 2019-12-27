using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class msw
	{
		public override void PostLoad()
        {
            m_actorMeshes = WResourceManager.LoadActorResource("Hanging Platform");
            base.PostLoad();
		}

		public override void PreSave()
		{

		}
	}
}
