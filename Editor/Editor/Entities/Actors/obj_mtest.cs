using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class obj_mtest
	{
		public override void PostLoad()
        {
            m_RegionAreaModel = WResourceManager.LoadObjResource("resources/editor/EditorCylinder.obj", new OpenTK.Vector4(0.5f, 1f, 1f, 1f), true, false);
        }

		public override void PreSave()
		{

		}
	}
}
