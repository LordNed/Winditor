using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class obj_movebox
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
            switch (Unknown_4)
            {
                case 0:
                case 4:
                case 8:
                case 9:
                    m_actorMeshes = WResourceManager.LoadActorResource("Wooden Crate");
                    break;
                case 1:
                case 11:
                    m_actorMeshes = WResourceManager.LoadActorResource("Black Box");
                    break;
                case 2:
                    m_actorMeshes = WResourceManager.LoadActorResource("Black Box With Statue on Top");
                    break;
                case 3:
                    m_actorMeshes = WResourceManager.LoadActorResource("Big Black Box");
                    break;
                case 5:
                    m_actorMeshes = WResourceManager.LoadActorResource("Golden Crate");
                    break;
                case 6:
                    m_actorMeshes = WResourceManager.LoadActorResource("Pushable Metal Box");
                    break;
                case 7:
                    m_actorMeshes = WResourceManager.LoadActorResource("Pushable Metal Box With Spring");
                    break;
                case 10:
                    m_actorMeshes = WResourceManager.LoadActorResource("Mirror");
                    break;
                case 12:
                    m_actorMeshes = WResourceManager.LoadActorResource("Mossy Black Box");
                    break;
                default:
                    m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new OpenTK.Vector4(1f, 1f, 1f, 1f));
                    break;
            }
        }
	}
}
