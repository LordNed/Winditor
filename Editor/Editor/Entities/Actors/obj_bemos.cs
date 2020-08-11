using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class obj_bemos
	{
		private short m_HeadRotation = 0;

		public override void PostLoad()
		{
			base.PostLoad();
			UpdateModel();
		}

		public override void PreSave()
		{

		}

		private void UpdateModel()
		{
			switch (Type)
			{
				case TypeEnum.Blue_Beamos:
					m_actorMeshes = WResourceManager.LoadActorResource("Blue Beamos");
					break;
				case TypeEnum.Red_Beamos:
					m_actorMeshes = WResourceManager.LoadActorResource("Red Beamos");
					break;
				case TypeEnum.Laser_Barrier:
					m_actorMeshes = WResourceManager.LoadActorResource("Laser Barrier");
					break;
			}
		}

		public override void Tick(float deltaTime)
		{
			base.Tick(deltaTime);

			if (IsSelected && Type == TypeEnum.Red_Beamos && m_actorMeshes.Count >= 3)
			{
				int turnSpeed;
				if (HeadRotationSpeed >= 0)
				{
					turnSpeed = HeadRotationSpeed * 250 + 750;
				} else
				{
					turnSpeed = HeadRotationSpeed * 250 - 750;
				}
				m_HeadRotation += (short)turnSpeed;

				float headRotationInDegrees = m_HeadRotation / 16384f * 90;
				m_actorMeshes[1].SetOffsetRotation(new Vector3(0, headRotationInDegrees, 0));
				m_actorMeshes[2].SetOffsetRotation(new Vector3(0, headRotationInDegrees, 0));
			}
		}

		public override void Draw(WSceneView view)
		{
			if (Type == TypeEnum.Laser_Barrier && LaserPath != null)
			{
				Matrix4 centerTrs = Matrix4.CreateTranslation(Transform.Position);
				m_objRender.Render(view.ViewMatrix, view.ProjMatrix, centerTrs);

				if (m_actorMeshes.Count > 0)
				{
					var wallMountModel = m_actorMeshes[0];

					for (int i = 0; i < 4; i++)
					{
						if (ColorOverrides.ColorsEnabled[i])
							wallMountModel.SetTevColorOverride(i, ColorOverrides.Colors[i]);

						if (ColorOverrides.ConstColorsEnabled[i])
							wallMountModel.SetTevkColorOverride(i, ColorOverrides.ConstColors[i]);
					}

					var points = LaserPath.GetPoints();
					if (points.Count < 2)
					{
						return;
					}

					Vector3 p1 = points[0].Transform.Position;
					Vector3 p2 = points[1].Transform.Position;
					Vector3 path_delta = p2 - p1;
					float y_rot1 = (float)Math.Atan2(path_delta.X, path_delta.Z);
					float y_rot2 = (float)Math.Atan2(-path_delta.X, -path_delta.Z);

					Quaternion firstPointRotation = Quaternion.FromAxisAngle(Vector3.UnitY, y_rot1);
					Quaternion secondPointRotation = Quaternion.FromAxisAngle(Vector3.UnitY, y_rot2);

					Matrix4 firstTrs = Matrix4.CreateFromQuaternion(firstPointRotation) * Matrix4.CreateTranslation(points[0].Transform.Position);
					Matrix4 secondTrs = Matrix4.CreateFromQuaternion(secondPointRotation) * Matrix4.CreateTranslation(points[1].Transform.Position);

					wallMountModel.Render(view.ViewMatrix, view.ProjMatrix, firstTrs);
					wallMountModel.Render(view.ViewMatrix, view.ProjMatrix, secondTrs);
				}
			} else
			{
				base.Draw(view);
			}
		}

		public override float GetBoundingRadius()
		{
			float radius = base.GetBoundingRadius();

			if (Type != TypeEnum.Laser_Barrier || LaserPath == null || m_actorMeshes.Count == 0)
				return radius;
			var points = LaserPath.GetPoints();
			if (points.Count < 2)
				return radius;

			for (int pathPointIndex = 0; pathPointIndex < 2; pathPointIndex++)
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
