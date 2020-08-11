using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class obj_vfan
	{
		public override void PostLoad()
		{
			base.PostLoad();
			m_actorMeshes = WResourceManager.LoadActorResource("Ganon's Tower Destructible Door");
		}

		public override void PreSave()
		{

		}
	}
}
