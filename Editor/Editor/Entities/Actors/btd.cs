using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class btd
	{
		public override void PostLoad()
		{
			UpdateModel();
		}

		public override void PreSave()
		{

		}

		private void UpdateModel()
        {
			m_actorMeshes = WResourceManager.LoadActorResource("Gohma");
		}
	}
}
