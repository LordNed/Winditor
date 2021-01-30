using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using OpenTK;

namespace WindEditor
{
	public partial class obj_canon
	{
		public override void PostLoad()
		{
			UpdateModel();
			m_actorMeshes = WResourceManager.LoadActorResource("Wall-Mounted Cannon");
			base.PostLoad();
		}

		public override void PreSave()
		{

		}

		private void UpdateModel()
		{
			float scale = 1f;
			if (ExtraScale != 255)
				scale += ExtraScale / 10f;
			VisualScaleMultiplier = new Vector3(scale);
		}
	}
}
