using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class icelift
	{
		public override void PostLoad()
		{
			UpdateModel();
			base.PostLoad();
		}

		public override void PreSave()
		{

		}

		private void UpdateModel()
		{
			if (Name == "Ylsic")
				m_actorMeshes = WResourceManager.LoadActorResource("Small Iceberg");
			else
				m_actorMeshes = WResourceManager.LoadActorResource("Large Iceberg");
		}
	}
}
