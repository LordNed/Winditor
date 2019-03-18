using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class jbo
	{
		public override void PostLoad()
		{
            m_actorMeshes = WResourceManager.LoadActorResource("Baba Bud");
		}

		public override void PreSave()
		{

		}
	}
}
