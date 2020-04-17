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
			m_RegionAreaModel = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new OpenTK.Vector4(1, 1, 1, 1));
			DisableRotationAndScaleForRaycasting = true;
		}

		public override void Draw(WSceneView view)
		{
			Matrix4 trs = Matrix4.CreateScale(VisualScale) * Matrix4.CreateFromQuaternion(Transform.Rotation) * Matrix4.CreateTranslation(Transform.Position);
			Matrix4 centerPointTrs = Matrix4.CreateTranslation(Transform.Position);

			m_objRender.Render(view.ViewMatrix, view.ProjMatrix, centerPointTrs);

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
