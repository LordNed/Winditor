using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class obj_ladder
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
            switch (Length)
            {
                case LengthEnum.Short:
                    m_actorMeshes = WResourceManager.LoadActorResource("Ladder 0");
                    break;
                case LengthEnum.Medium:
                    m_actorMeshes = WResourceManager.LoadActorResource("Ladder 1");
                    break;
                case LengthEnum.Long:
                    m_actorMeshes = WResourceManager.LoadActorResource("Ladder 2");
                    break;
                case LengthEnum.Very_Long:
                    m_actorMeshes = WResourceManager.LoadActorResource("Ladder 3");
                    break;
                case LengthEnum.Very_Short:
                    m_actorMeshes = WResourceManager.LoadActorResource("Ladder 4");
                    break;
                default:
                    m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new OpenTK.Vector4(1f, 1f, 1f, 1f));
                    break;
            }
        }
	}
}
