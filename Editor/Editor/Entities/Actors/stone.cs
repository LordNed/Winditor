using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class stone
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
            switch (Unknown_5)
            {
                case 0:
                    m_actorMeshes = WResourceManager.LoadActorResource("Small Rock");
                    break;
                case 1:
                    m_actorMeshes = WResourceManager.LoadActorResource("Small Black Rock");
                    break;
                case 2:
                    m_actorMeshes = WResourceManager.LoadActorResource("Boulder");
                    break;
                case 3:
                    m_actorMeshes = WResourceManager.LoadActorResource("Head Boulder");
                    break;
                case 4:
                    m_actorMeshes = WResourceManager.LoadActorResource("Small Boulder");
                    break;
                default:
                    m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new OpenTK.Vector4(1f, 1f, 1f, 1f));
                    break;
            }
        }
	}
}
