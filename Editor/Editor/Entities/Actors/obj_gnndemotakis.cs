using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class obj_gnndemotakis
	{
		public override void PostLoad()
		{
			m_actorMeshes = WResourceManager.LoadActorResource("Ganon Waterfall S");
			base.PostLoad();
		}

		public override void PreSave()
		{

		}
	}
}
