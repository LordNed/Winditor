using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class obj_hole
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
			m_actorMeshes.Clear();
			m_objRender = null;
			if (HasVisibleHole)
			{
				m_actorMeshes = WResourceManager.LoadActorResource("Pitfall");
			} else
			{
				m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new OpenTK.Vector4(1f, 1f, 1f, 1f));
			}

			if (HoleScale == 0xFFFF)
			{
				VisualScaleMultiplier = Vector3.One;
			} else
			{
				VisualScaleMultiplier = new Vector3(1 + HoleScale / 6.5f, 1 + HoleScale / 6.5f, 1 + HoleScale / 6.5f);
			}
		}

		protected override Vector3 VisualScale
		{
			get { return Vector3.Multiply(VisualScaleMultiplier, Transform.LocalScale.X); }
		}
	}
}
