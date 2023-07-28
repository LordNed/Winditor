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
	public partial class obj_hlift
    {
        #region Constants
        private static readonly int[] kPlatformHeights = {
            125,
            250,
            375,
            500,
            625,
            750,
            875,
            1000,
        };
        #endregion

        private J3D m_liftMesh;

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
            m_liftMesh = null;
            List<J3D> meshes;
            if (Size == SizeEnum.Big)
            {
                meshes = WResourceManager.LoadActorResource("Big Lift");
            }
            else
            {
                meshes = WResourceManager.LoadActorResource("Small Lift");
            }
            if (meshes != null && meshes.Count > 0)
                m_liftMesh = meshes[0];
        }

        public override void Draw(WSceneView view)
        {
            base.Draw(view); // Draw the default editor cube so the user knows where to click to select it.
            if (m_liftMesh == null)
                return;

            Vector3 modelOffset = new Vector3(0, kPlatformHeights[Height], 0);

            Matrix4 trs = Matrix4.CreateScale(VisualScale) *
                Matrix4.CreateFromQuaternion(Transform.Rotation.ToSinglePrecision()) *
                Matrix4.CreateTranslation(Transform.Position + modelOffset);

            m_liftMesh.Render(view.ViewMatrix, view.ProjMatrix, trs);
        }

        public override float GetBoundingRadius()
        {
            return base.GetBoundingRadius() + kPlatformHeights[Height];
        }
    }
}
