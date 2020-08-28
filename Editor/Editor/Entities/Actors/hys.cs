using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using OpenTK;

namespace WindEditor
{
	public partial class hys
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
			m_actorMeshes = WResourceManager.LoadActorResource("Eye Switch");

			if (Unknown_2 == 1)
				VisualScaleMultiplier = new Vector3(2);
		}
	}
}
