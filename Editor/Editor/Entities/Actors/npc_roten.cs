using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class npc_roten
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
			m_actorMeshes.Clear();
			m_objRender = null;

			switch(Unknown_2)
			{
				case 0:
					m_actorMeshes = WResourceManager.LoadActorResource("Goron A");
					break;
				case 1:
					m_actorMeshes = WResourceManager.LoadActorResource("Goron B");
					break;
				case 2:
					m_actorMeshes = WResourceManager.LoadActorResource("Goron C");
					break;
				default:
					m_actorMeshes = WResourceManager.LoadActorResource("Goron A");
					break;
			}
		}
	}
}
