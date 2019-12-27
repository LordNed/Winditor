using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor
{
    public partial class stone2
    {
        public override void PostLoad()
        {
            UpdateModel();
            base.PostLoad();
        }

        private void UpdateModel()
        {
            m_actorMeshes.Clear();
            m_objRender = null;
            switch (Unknown_4)
            {
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
