using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class obj_gaship2
	{
		public override void PostLoad()
		{
			m_actorMeshes = WResourceManager.LoadActorResource("Forsaken Fortress Ship");
			base.PostLoad();
		}

		public override void PreSave()
		{

		}
	}
}
