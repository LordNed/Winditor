using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class npc_km1
	{
		public override void PostLoad()
		{
			m_actorMeshes = WResourceManager.LoadActorResource("Rich Mila");
			base.PostLoad();
		}

		public override void PreSave()
		{

		}
	}
}
