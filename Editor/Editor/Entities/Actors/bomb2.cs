using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class bomb2
	{
		public override void PostLoad()
		{
            m_actorMeshes = WResourceManager.LoadActorResource("Bomb from a Bomb Flower");
        }

		public override void PreSave()
		{

		}
	}
}
