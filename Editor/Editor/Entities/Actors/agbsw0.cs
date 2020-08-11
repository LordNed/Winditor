using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using OpenTK;

namespace WindEditor
{
	public partial class agbsw0
	{
		public override void PostLoad()
		{
			base.PostLoad();

            UpdateModel();
        }

        public override void PreSave()
		{

        }

        public override void AddToRenderer(WSceneView view)
        {
            view.AddTransparentMesh(this);
        }

        private void UpdateModel()
        {
            // Note: Not sure if all of the types are cylinders.
            m_RegionAreaModel = WResourceManager.LoadObjResource("resources/editor/EditorCylinder.obj", new Vector4(1f, 1f, 1f, 1f), true, false);

            if (Type == TypeEnum.agbCSW)
            {
                VisualScaleMultiplier = new Vector3(8000f / 50f, 8000f / 50f, 8000f / 50f);
            }
            else
            {
                VisualScaleMultiplier = new Vector3(200f / 50f, 200f / 50f, 200f / 50f);
            }
        }
    }
}
