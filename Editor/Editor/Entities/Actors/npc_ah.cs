using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class npc_ah
	{
		public override void PostLoad()
		{
			m_actorMeshes = WResourceManager.LoadActorResource("Old Man Ho-Ho");
			base.PostLoad();
		}

		public override void PreSave()
		{

		}
	}
}
