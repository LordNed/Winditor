using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class kytag01
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
			get {
				float largerScale = Transform.LocalScale.X;
				if (Transform.LocalScale.Z > Transform.LocalScale.X) {
					largerScale = Transform.LocalScale.Z;
				}
				return Vector3.Multiply(new Vector3(largerScale, 1f, largerScale), VisualScaleMultiplier);
			}
		}
	}
}
