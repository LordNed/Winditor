using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class tag_etc
	{
		public override void PostLoad()
		{
			m_RegionAreaModel = WResourceManager.LoadObjResource("resources/editor/EditorCylinder.obj", new Vector4(0.5f, 0.5f, 1f, 1f), true, false);
			VisualScaleMultiplier = new Vector3(100f / 100f, 100f / 100f, 100f / 100f);
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
