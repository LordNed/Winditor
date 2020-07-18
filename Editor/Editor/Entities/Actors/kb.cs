using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class kb
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
			switch (Color)
			{
				case ColorEnum.Pink:
					m_actorMeshes = WResourceManager.LoadActorResource("Pink Pig");
					break;
				case ColorEnum.Speckled:
					m_actorMeshes = WResourceManager.LoadActorResource("Speckled Pig");
					break;
				case ColorEnum.Black:
					m_actorMeshes = WResourceManager.LoadActorResource("Black Pig");
					break;
				default:
					m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new OpenTK.Vector4(1f, 1f, 1f, 1f));
					break;
			}
		}
	}
}
