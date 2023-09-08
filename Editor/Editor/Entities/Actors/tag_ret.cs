using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using OpenTK;

namespace WindEditor
{
	public partial class tag_ret
	{
		public override void PostLoad()
		{
			base.PostLoad();
            m_RegionAreaModel = WResourceManager.LoadObjResource("resources/editor/EditorCylinderBottomOrigin.obj", new Vector4(0f, 1f, 1f, 1f), true, false);
            VisualScaleMultiplier = new Vector3(1000f / 100f, 100f / 100f, 1000f / 100f);
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
