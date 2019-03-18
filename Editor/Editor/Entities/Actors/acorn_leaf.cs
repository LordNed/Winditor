using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class acorn_leaf
	{
		public override void PostLoad()
		{
            m_actorMeshes = WResourceManager.LoadActorResource("Nut Generator");
            m_actorMeshes.AddRange(WResourceManager.LoadActorResource("Nut"));
		}

		public override void PreSave()
		{

		}
	}
}
