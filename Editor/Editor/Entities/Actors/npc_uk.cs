using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class npc_uk
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
            switch (WhichKillerBee)
            {
                case WhichKillerBeeEnum.Jin:
                    m_actorMeshes = WResourceManager.LoadActorResource("Jin");
                    break;
                case WhichKillerBeeEnum.Jan:
                    m_actorMeshes = WResourceManager.LoadActorResource("Jan");
                    break;
                case WhichKillerBeeEnum.JunRoberto:
                    m_actorMeshes = WResourceManager.LoadActorResource("Jun-Roberto");
                    break;
                default:
                    m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new OpenTK.Vector4(1f, 1f, 1f, 1f));
                    break;
            }
        }
	}
}
