using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class obj_mkie
	{
		public override void PostLoad()
		{
            m_actorMeshes = WResourceManager.LoadActorResource("Big Elephant Statue");

			if (Unknown_2 == 1)
				Transform.ScaleBase.BackingVector *= 3;
        }

		public override void PreSave()
		{

		}
	}
}
