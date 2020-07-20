using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class oship
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
			if (IsGolden)
			{
				m_actorMeshes = WResourceManager.LoadActorResource("Golden Gunboat");
			} else
			{
				m_actorMeshes = WResourceManager.LoadActorResource("Gunboat");
			}
		}
	}
}
