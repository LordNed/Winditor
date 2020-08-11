using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class kytag00
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
			m_RegionAreaModel = WResourceManager.LoadObjResource("resources/editor/EditorCylinderBottomOrigin.obj", new Vector4(0.5f, 0.5f, 1f, 1f), true, false);
			VisualScaleMultiplier = new Vector3(5000f / 100f, 5000f / 100f / 2f, 5000f / 100f);
		}

		protected override Vector3 VisualScale
		{
			get { return Vector3.Multiply(new Vector3(Transform.LocalScale.X, Transform.LocalScale.Y, Transform.LocalScale.X), VisualScaleMultiplier); }
		}
	}
}
