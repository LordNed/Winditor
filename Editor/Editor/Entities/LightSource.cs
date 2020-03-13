using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor
{
    public partial class LightSource
    {
        public override void PostLoad()
        {
            m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new Vector4(1, 0.5f, 0, 1), true);
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
