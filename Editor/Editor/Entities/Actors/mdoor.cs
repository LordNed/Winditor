using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class mdoor
	{
		public override void PostLoad()
		{
            m_actorMeshes = WResourceManager.LoadActorResource("Wooden Gate");
		}

		public override void PreSave()
		{

		}
	}
}
