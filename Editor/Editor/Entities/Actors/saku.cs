using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class saku
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
			switch (Name)
			{
				case "Dsaku":
					if (Unknown_1 == 1)
						m_actorMeshes = WResourceManager.LoadActorResource("Strong Wooden Barrier 2x");
					else
						m_actorMeshes = WResourceManager.LoadActorResource("Strong Wooden Barrier 1x");
					break;
				case "Ksaku":
					if (Unknown_1 == 1)
						m_actorMeshes = WResourceManager.LoadActorResource("Weak Wooden Barrier 2x");
					else
						m_actorMeshes = WResourceManager.LoadActorResource("Weak Wooden Barrier 1x");
					break;
				default:
					break;
			}
		}
	}
}
