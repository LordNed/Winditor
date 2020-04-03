using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using OpenTK;
using JStudio.J3D;

namespace WindEditor
{
    public partial class klft
    {
        private J3D m_liftControlMesh;

        public override void PostLoad()
        {
            m_actorMeshes = WResourceManager.LoadActorResource("Pulley Lift");
            m_liftControlMesh = null;
            var meshes = WResourceManager.LoadActorResource("Pulley Lift Control");
            if (meshes != null && meshes.Count > 0)
                m_liftControlMesh = meshes[0];

            base.PostLoad();
        }

        public override void PreSave()
        {

        }

        public override void Draw(WSceneView view)
        {
            base.Draw(view);

            if (m_liftControlMesh == null)
                return;

            for (int i = 0; i < 4; i++)
            {
                if (ColorOverrides.ColorsEnabled[i])
                    m_liftControlMesh.SetTevColorOverride(i, ColorOverrides.Colors[i]);

                if (ColorOverrides.ConstColorsEnabled[i])
                    m_liftControlMesh.SetTevkColorOverride(i, ColorOverrides.ConstColors[i]);
            }

            var points = Path.GetPoints();
            for (int pathPointIndex = 2; pathPointIndex < 4; pathPointIndex++)
            {
                if (pathPointIndex >= points.Count)
                    break;
                PathPoint_v2 point = points[pathPointIndex];

                Matrix4 trs = Matrix4.CreateScale(point.Transform.LocalScale) * Matrix4.CreateFromQuaternion(point.Transform.Rotation) * Matrix4.CreateTranslation(point.Transform.Position);

                m_liftControlMesh.Render(view.ViewMatrix, view.ProjMatrix, trs);
            }
        }

        public override float GetBoundingRadius()
        {
            float radius = base.GetBoundingRadius();

            var points = Path.GetPoints();
            for (int pathPointIndex = 2; pathPointIndex < 4; pathPointIndex++)
            {
                if (pathPointIndex >= points.Count)
                    break;
                PathPoint_v2 point = points[pathPointIndex];

                float dist = (Transform.Position - point.Transform.Position).Length;
                if (dist > radius)
                    radius = dist;
            }

            return radius;
        }
    }
}
