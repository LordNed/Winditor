using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class npc_kk1
	{
		public override void PostLoad()
		{
			m_actorMeshes = WResourceManager.LoadActorResource("Poor Mila");
			base.PostLoad();
		}

		public override void PreSave()
		{

		}
	}
}
