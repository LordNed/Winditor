using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class boss_item
	{
		public override void PostLoad()
		{
            m_actorMeshes = WResourceManager.LoadActorResource("Heart Container");
        }

		public override void PreSave()
		{

		}
	}
}
