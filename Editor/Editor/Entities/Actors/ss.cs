using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class ss
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
			if (Type == TypeEnum.Blocks_Chest)
			{
				m_actorMeshes = WResourceManager.LoadActorResource("Eye-Vine Chest Blocker");
			}
			else
			{
				m_actorMeshes = WResourceManager.LoadActorResource("Eye-Vine Door Blocker");
			}
		}
	}
}
