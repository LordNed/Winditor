using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class obj_ikada
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
                case TypeEnum.Raft:
                    m_actorMeshes = WResourceManager.LoadActorResource("Raft");
                    break;
                case TypeEnum.Beedles_Shop_Ship:
                    m_actorMeshes = WResourceManager.LoadActorResource("Beedle's Shop Ship");
                    break;
                case TypeEnum.Submarine:
                    m_actorMeshes = WResourceManager.LoadActorResource("Submarine");
                    break;
                case TypeEnum.Beedles_Special_Shop_Ship:
                    m_actorMeshes = WResourceManager.LoadActorResource("Beedle's Special Shop Ship");
                    break;
                case TypeEnum.Salvage_Corp_Ship:
                    m_actorMeshes = WResourceManager.LoadActorResource("Salvage Corp Ship");
                    break;
                default:
                    m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new OpenTK.Vector4(1f, 1f, 1f, 1f));
                    break;
            }
        }
	}
}
