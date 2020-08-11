using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class obj_kanoke
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
			if (IsUpright)
			{
				m_actorMeshes = WResourceManager.LoadActorResource("Upright Coffin");
			}
			else
			{
				m_actorMeshes = WResourceManager.LoadActorResource("Coffin");
			}
		}
	}
}
