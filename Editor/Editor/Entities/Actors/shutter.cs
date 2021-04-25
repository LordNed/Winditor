using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class shutter
	{
		public override void PostLoad()
		{
			UpdateModel();
			base.PostLoad();
		}

		public override void PreSave()
		{

		}

		private void UpdateModel()
		{
			m_actorMeshes.Clear();
			m_objRender = null;
			switch (Model)
            {
				case ModelEnum.Large_Shutter:
					m_actorMeshes = WResourceManager.LoadActorResource("Large Shutter");
					break;
				case ModelEnum.Small_Shutter:
					m_actorMeshes = WResourceManager.LoadActorResource("Small Shutter");
					break;
				default:
					m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new OpenTK.Vector4(1f, 1f, 1f, 1f));
					break;
			}
		}
	}
}
