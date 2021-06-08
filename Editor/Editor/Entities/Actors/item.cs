using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor
{
    public partial class item
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
            var models = WResourceManager.LoadModelForItem(ItemID);
            if (models == null)
                m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new OpenTK.Vector4(1f, 1f, 1f, 1f));
            else
                m_actorMeshes = models;
        }
    }
}
