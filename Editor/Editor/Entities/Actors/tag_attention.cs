using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using OpenTK;

namespace WindEditor
{
	public partial class tag_attention
	{
        public override void PostLoad()
        {
            if (Name == "AttTag")
            {
                Transform.LocalScale = new Vector3(Transform.LocalScale.X, Transform.LocalScale.Y, Transform.LocalScale.X);
                m_RegionAreaModel = WResourceManager.LoadObjResource("resources/editor/EditorCylinder.obj", new OpenTK.Vector4(0.5f, 0.75f, 1f, 1f), true, false);
            }
            else if (Name == "AttTagB")
            {
                Transform.LocalScale = new Vector3(Transform.LocalScale.X, Transform.LocalScale.Y, Transform.LocalScale.X);
                m_RegionAreaModel = WResourceManager.LoadObjResource("resources/editor/EditorCylinder.obj", new OpenTK.Vector4(0.5f, 0.75f, 1f, 1f), true, false);
            }
        }

        public override void PreSave()
        {

        }
    }
}
