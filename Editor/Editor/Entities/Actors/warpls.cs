using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using OpenTK;

namespace WindEditor
{
	public partial class warpls
	{
		public override void PostLoad()
        {
			UpdateModel();
            base.PostLoad();
		}

		public override void PreSave()
		{

		}

		public void UpdateModel()
		{
			if (Type == TypeEnum.Pink_Warp)
			{
				VisualScaleMultiplier = new Vector3(2f, 1f, 2f);
				m_actorMeshes = WResourceManager.LoadActorResource("Pink Light Beam Warp");

			} else
			{
				VisualScaleMultiplier = new Vector3(1f, 1f, 1f);
				m_actorMeshes = WResourceManager.LoadActorResource("White Light Beam Warp");
			}
		}

		public override void AddToRenderer(WSceneView view)
		{
			view.AddTransparentMesh(this);
		}
	}
}
