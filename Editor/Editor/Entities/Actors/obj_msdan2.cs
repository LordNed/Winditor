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
	public partial class obj_msdan2
	{
		private J3D m_stairMesh;

		public override void PostLoad()
		{
			m_stairMesh = null;
            m_objRender = null;
			var meshes = WResourceManager.LoadActorResource("Earth Temple Stair");
			if (meshes != null && meshes.Count > 0)
				m_stairMesh = meshes[0];

			base.PostLoad();
		}

		public override void PreSave()
		{

		}

        public override void Draw(WSceneView view)
        {
            base.Draw(view);
            if (m_stairMesh == null)
                return;

            for (int i = 0; i < 4; i++)
            {
                if (ColorOverrides.ColorsEnabled[i])
                    m_stairMesh.SetTevColorOverride(i, ColorOverrides.Colors[i]);

                if (ColorOverrides.ConstColorsEnabled[i])
                    m_stairMesh.SetTevkColorOverride(i, ColorOverrides.ConstColors[i]);
            }

            for (int i = 1; i < 16; i++)
            {
                WTransform transform = new WTransform();
                float yRot = Transform.Rotation.ToEulerAngles().Y;// + 180;
                transform.Rotation = Quaternion.FromAxisAngle(Vector3.UnitY, (float)(yRot / 180f * Math.PI));
                Vector3 positionOffset = new Vector3(0, 0, i * -50f);
                positionOffset = Vector3.Transform(positionOffset, transform.Rotation);
                transform.Position = Transform.Position + positionOffset;

                Matrix4 trs = Matrix4.CreateScale(transform.LocalScale) * Matrix4.CreateFromQuaternion(transform.Rotation) * Matrix4.CreateTranslation(transform.Position);

                m_stairMesh.Render(view.ViewMatrix, view.ProjMatrix, trs);
            }
        }

        public override float GetBoundingRadius()
        {
            return 32 * 50f;
        }
    }
}
