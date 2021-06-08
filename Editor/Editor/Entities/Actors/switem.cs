using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class switem
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
			List<JStudio.J3D.J3D> models = null;
			if ((int)SpawnedItem <= 0x1F)
				models = WResourceManager.LoadModelForItem((ItemID)SpawnedItem);
			if (models == null)
				m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new OpenTK.Vector4(1f, 1f, 1f, 1f));
			else
				m_actorMeshes = models;
		}
	}
}
