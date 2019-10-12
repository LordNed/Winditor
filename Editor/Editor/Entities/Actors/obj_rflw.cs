using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class obj_rflw
	{
		public override void PostLoad()
		{
            m_actorMeshes = WResourceManager.LoadActorResource("Fancy Potted Plant");
            base.PostLoad();
		}

		public override void PreSave()
		{

		}
	}
}
