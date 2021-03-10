using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class npc_bs1
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
			if (IsMaskedBeedle)
			{
				m_actorMeshes = WResourceManager.LoadActorResource("Masked Beedle");
			}
			else
			{
				m_actorMeshes = WResourceManager.LoadActorResource("Beedle");
			}
		}
	}
}
