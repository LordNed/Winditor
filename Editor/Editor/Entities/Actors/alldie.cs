using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using OpenTK;

namespace WindEditor
{
	public partial class alldie
	{
		public override void PostLoad()
        {
            m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new Vector4(0, 1, 0, 1), true);
		}

		public override void PreSave()
		{

        }

        public override void AddToRenderer(WSceneView view)
        {
            view.AddTransparentMesh(this);
        }
    }
}
