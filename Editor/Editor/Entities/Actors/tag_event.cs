using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using OpenTK;

namespace WindEditor
{
	public partial class tag_event
	{
		public override void PostLoad()
		{
			m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new Vector4(1f, 0.5f, 1f, 1f), true, false);
			m_RegionAreaModel = WResourceManager.LoadObjResource("resources/editor/EditorCylinder.obj", new Vector4(1f, 0f, 1f, 1f), true, false);
			base.PostLoad();
		}

		public override void PreSave()
		{

		}

		protected override Vector3 VisualScale
		{
			get { return Vector3.Multiply(new Vector3(Transform.LocalScale.X, Transform.LocalScale.Y, Transform.LocalScale.X), VisualScaleMultiplier); }
		}
	}
}
