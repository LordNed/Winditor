using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using OpenTK;

namespace WindEditor
{
	public partial class tag_light
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
            VisualScaleMultiplier = Vector3.One;
            if (Type == TypeEnum.Light_Beam)
            {
                VisualScaleMultiplier = new Vector3(1f, 2f, 1f);
                if (Unknown_4 >= 9)
                {
                    m_actorMeshes = WResourceManager.LoadActorResource("Light Ray Cylinder");
                }
                else
                {
                    m_actorMeshes = WResourceManager.LoadActorResource("Light Ray Cone");
                }
            }
            else
            {
                m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new OpenTK.Vector4(1f, 1f, 1f, 1f));
            }
        }

        public override void AddToRenderer(WSceneView view)
        {
            if (Type == TypeEnum.Light_Beam)
            {
                view.AddTransparentMesh(this);
            }
            else
            {
                view.AddOpaqueMesh(this);
            }
        }
    }
}
