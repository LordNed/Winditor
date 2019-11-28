using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class npc_gp1
	{
		public override void PostLoad()
		{
            m_actorMeshes = WResourceManager.LoadActorResource("Rich Maggie's Father");
            base.PostLoad();
		}

		public override void PreSave()
		{

		}
	}
}
