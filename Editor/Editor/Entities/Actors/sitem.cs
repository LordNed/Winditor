using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class sitem
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
			switch(Unknown_1)
			{
				case 0:
					m_actorMeshes = WResourceManager.LoadActorResource("Tentacle Nut");
					break;
				case 1:
					m_actorMeshes = WResourceManager.LoadActorResource("Spiked Tentacle Nut");
					break;
				case 2:
					m_actorMeshes = WResourceManager.LoadActorResource("Tentacle Pinecone");
					break;
				default:
					m_actorMeshes = WResourceManager.LoadActorResource("Tentacle Nut");
					break;
			}
		}
	}
}
