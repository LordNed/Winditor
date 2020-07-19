using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class npc_kamome
	{
		public override void PostLoad()
		{
			base.PostLoad();
			m_RegionAreaModel = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new Vector4(1f, 1f, 1f, 1f), true, false);
			VisualScaleMultiplier = new Vector3(1000f / 50f, 1000f / 50f, 1000f / 50f);
		}

		public override void PreSave()
		{

		}
	}
}
