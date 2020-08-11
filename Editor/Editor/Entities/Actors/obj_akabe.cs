using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class obj_akabe
	{
		public override void PostLoad()
		{
			base.PostLoad();
			m_RegionAreaModel = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new OpenTK.Vector4(1, 1, 1, 1), true, false);
		}

		public override void PreSave()
		{

		}
	}
}
