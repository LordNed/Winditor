using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace WindEditor
{
	public class TriggerRegion : Actor
	{
		protected SimpleObjRenderer m_RegionAreaModel;

		// Constructor
		public TriggerRegion(FourCC fourCC, WWorld world) : base(fourCC, world)
		{
			m_RegionAreaModel = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new OpenTK.Vector4(1, 1, 1, 1), true, false);
			DisableRotationAndScaleForRaycasting = true;
		}

		public override void Draw(WSceneView view)
		{
			Matrix4 trs = Matrix4.CreateScale(VisualScale) * Matrix4.CreateFromQuaternion(Transform.Rotation) * Matrix4.CreateTranslation(Transform.Position);
			Matrix4 centerPointTrs = Matrix4.CreateTranslation(Transform.Position);

			// Render the center point model first.
			if (m_actorMeshes.Count > 0)
			{
				foreach (var actor_mesh in m_actorMeshes)
				{
					for (int i = 0; i < 4; i++)
					{
						if (ColorOverrides.ColorsEnabled[i])
							actor_mesh.SetTevColorOverride(i, ColorOverrides.Colors[i]);

						if (ColorOverrides.ConstColorsEnabled[i])
							actor_mesh.SetTevkColorOverride(i, ColorOverrides.ConstColors[i]);
					}

					if (IsSelected)
						actor_mesh.Tick(1 / (float)60);

					actor_mesh.Render(view.ViewMatrix, view.ProjMatrix, centerPointTrs);
				}
			}
			else if (m_objRender != null)
				m_objRender.Render(view.ViewMatrix, view.ProjMatrix, centerPointTrs);

			// Then render the area, but only when selected.
			if (IsSelected)
			{
				m_RegionAreaModel.Render(view.ViewMatrix, view.ProjMatrix, trs);
			}
		}

		public override void AddToRenderer(WSceneView view)
		{
			view.AddTransparentMesh(this);
		}
	}
}
