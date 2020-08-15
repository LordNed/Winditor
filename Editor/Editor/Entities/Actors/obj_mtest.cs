using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using OpenTK;

namespace WindEditor
{
	public partial class obj_mtest
	{
		public override void PostLoad()
        {
			UpdateModel();
		}

		public override void PreSave()
		{

		}

		private void UpdateModel()
		{
			switch (Type)
			{
				case TypeEnum.Water_Splashes:
					m_RegionAreaModel = WResourceManager.LoadObjResource("resources/editor/EditorCylinder.obj", new OpenTK.Vector4(0.5f, 1f, 1f, 1f), true, false);
					VisualScaleMultiplier = new Vector3(5f, 10f, 5f);
					break;
				// TODO
				default:
					m_RegionAreaModel = WResourceManager.LoadObjResource("resources/editor/EditorCylinder.obj", new OpenTK.Vector4(0.5f, 1f, 1f, 1f), true, false);
					VisualScaleMultiplier = new Vector3(5f, 10f, 5f);
					break;
			}
		}
	}
}
