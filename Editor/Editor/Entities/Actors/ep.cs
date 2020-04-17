using JStudio.J3D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
    public partial class ep
    {
        private J3D m_brazierModel;

        public override void PostLoad()
        {
            base.PostLoad();
            m_RegionAreaModel = WResourceManager.LoadObjResource("resources/editor/EditorSphere_Trigger.obj", new OpenTK.Vector4(1f, 0.7f, 0.4f, 1f), true, false);
            VisualScaleMultiplier = new Vector3(1.5f, 1.5f, 1.5f);
            UpdateModel();
        }

        public override void PreSave()
		{

        }

        public override void AddToRenderer(WSceneView view)
        {
            view.AddTransparentMesh(this);
        }

        protected bool HasBrazier()
        {
            return Type == TypeEnum.Has_brazier_1 || Type == TypeEnum.Has_brazier_2 || Type == TypeEnum.Has_brazier_1_B;
        }

        public void UpdateModel()
        {
            m_brazierModel = null;
            m_objRender = null;
            m_actorMeshes = null;
            if (HasBrazier())
            {
                if (IsWooden)
                    m_actorMeshes = WResourceManager.LoadActorResource("Wooden Torch");
                else
                    m_actorMeshes = WResourceManager.LoadActorResource("Golden Torch");
            }
            if (m_actorMeshes == null)
                m_actorMeshes = new List<J3D>();

            if (m_actorMeshes.Count > 0)
                m_brazierModel = m_actorMeshes[0];
            else
                m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new OpenTK.Vector4(1, 1, 1, 1));
        }

        public override void Draw(WSceneView view)
        {
            if (HasBrazier() && m_brazierModel != null)
            {
                // Draw the brazier.
                Matrix4 trs = Matrix4.CreateFromQuaternion(Transform.Rotation) * Matrix4.CreateTranslation(Transform.Position);

                for (int i = 0; i < 4; i++)
                {
                    if (ColorOverrides.ColorsEnabled[i])
                        m_brazierModel.SetTevColorOverride(i, ColorOverrides.Colors[i]);

                    if (ColorOverrides.ConstColorsEnabled[i])
                        m_brazierModel.SetTevkColorOverride(i, ColorOverrides.ConstColors[i]);
                }

                if (IsSelected)
                    m_brazierModel.Tick(1 / (float)60);

                m_brazierModel.Render(view.ViewMatrix, view.ProjMatrix, trs);
            }

            // Draw the region that is lit up.
            base.Draw(view);
        }

        protected override Vector3 VisualScale
        {
            get { return Vector3.Mult(VisualScaleMultiplier, Transform.LocalScale.X); }
        }
    }
}
