using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class am2
	{
		public override void PostLoad()
        {
            m_actorMeshes = WResourceManager.LoadActorResource("Armos");
            base.PostLoad();
		}

		public override void PreSave()
		{

		}
	}
}
