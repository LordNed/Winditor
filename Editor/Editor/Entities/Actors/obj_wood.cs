using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using OpenTK.Graphics.OpenGL;

namespace WindEditor
{
	public partial class obj_wood
	{
		public override void PostLoad()
        {
            base.PostLoad();
            string model_path = "resources/models/foliage/bush.obj";
            m_objRender = WResourceManager.LoadObjResource(model_path, new OpenTK.Vector4(1, 1, 1, 1), true, false, TextureWrapMode.MirroredRepeat);
        }

		public override void PreSave()
		{

		}
	}
}
