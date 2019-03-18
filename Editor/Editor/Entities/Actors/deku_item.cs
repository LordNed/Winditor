using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class deku_item
	{
		public override void PostLoad()
		{
            m_actorMeshes = WResourceManager.LoadActorResource("Deku Leaf Pickup");
		}

		public override void PreSave()
		{

		}
	}
}
