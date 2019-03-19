using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class warpf
	{
		public override void PostLoad()
		{
            m_actorMeshes = WResourceManager.LoadActorResource("Tower of the Gods Boss Warp");
        }

		public override void PreSave()
		{

		}
	}
}
