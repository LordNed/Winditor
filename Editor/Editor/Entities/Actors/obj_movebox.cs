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
            switch (Type)
            {
                case TypeEnum.Breakable_Wooden_Crate:
                case TypeEnum.Unbreakable_Wooden_Crate_A:
                case TypeEnum.Unbreakable_Wooden_Crate_B:
                case TypeEnum.Unbreakable_Wooden_Crate_C:
                    m_actorMeshes = WResourceManager.LoadActorResource("Wooden Crate");
                    break;
                case TypeEnum.Black_Box_A:
                case TypeEnum.Black_Box_B:
                    m_actorMeshes = WResourceManager.LoadActorResource("Black Box");
                    break;
                case TypeEnum.Black_Box_With_Statue_on_Top:
                    m_actorMeshes = WResourceManager.LoadActorResource("Black Box With Statue on Top");
                    break;
                case TypeEnum.Big_Black_Box:
                    m_actorMeshes = WResourceManager.LoadActorResource("Big Black Box");
                    break;
                case TypeEnum.Golden_Crate:
                    m_actorMeshes = WResourceManager.LoadActorResource("Golden Crate");
                    break;
                case TypeEnum.Metal_Box:
                    m_actorMeshes = WResourceManager.LoadActorResource("Pushable Metal Box");
                    break;
                case TypeEnum.Metal_Box_With_Spring:
                    m_actorMeshes = WResourceManager.LoadActorResource("Pushable Metal Box With Spring");
                    break;
                case TypeEnum.Mirror:
                    m_actorMeshes = WResourceManager.LoadActorResource("Mirror");
                    break;
                case TypeEnum.Mossy_Black_Box:
                    m_actorMeshes = WResourceManager.LoadActorResource("Mossy Black Box");
                    break;
                default:
                    m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new OpenTK.Vector4(1f, 1f, 1f, 1f));
                    break;
            }
        }
	}
}
