using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class obj_gryw00
	{
		public override void PostLoad()
		{
			// Commented out because using the actual model makes it hidden and hard to click
			//m_actorMeshes = WResourceManager.LoadActorResource("Lake Outside Dragon Roost Cavern");
			base.PostLoad();
		}

		public override void PreSave()
		{

		}
	}
}
