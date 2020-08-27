using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using OpenTK;

namespace WindEditor
{
	public partial class obj_mkie
	{
		public override void PostLoad()
		{
			UpdateModel();
        }

		public override void PreSave()
		{

		}

		private void UpdateModel()
		{
			m_actorMeshes = WResourceManager.LoadActorResource("Big Elephant Statue");

			if (Unknown_2 == 1)
				VisualScaleMultiplier = new Vector3(3);
			else
				VisualScaleMultiplier = new Vector3(1);
		}
	}
}
