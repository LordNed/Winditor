using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class obj_akabe
	{
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
			switch (CollisionArchive)
			{
				case CollisionArchiveEnum.Akabe:
					m_RegionAreaModel = WResourceManager.LoadObjResource("resources/editor/EditorVerticalPlaneBottomOrigin.obj", new OpenTK.Vector4(1, 1, 1, 1), true, false);
					VisualScaleMultiplier = new Vector3(1f, 1f, 1f);
					break;
				case CollisionArchiveEnum.AkabeD:
					m_RegionAreaModel = WResourceManager.LoadObjResource("resources/editor/EditorVerticalPlaneBottomOrigin.obj", new OpenTK.Vector4(1, 1, 1, 1), true, false);
					VisualScaleMultiplier = new Vector3(10f, 10f, 1f);
					break;
				case CollisionArchiveEnum.AkabeK:
					m_RegionAreaModel = WResourceManager.LoadObjResource("resources/editor/EditorVerticalPlaneBottomOrigin.obj", new OpenTK.Vector4(1, 1, 1, 1), true, false);
					VisualScaleMultiplier = new Vector3(1f, 1f, 1f);
					break;
				case CollisionArchiveEnum.NBOX:
					m_RegionAreaModel = WResourceManager.LoadObjResource("resources/editor/EditorCubeBottomOrigin.obj", new OpenTK.Vector4(1, 1, 1, 1), true, false);
					VisualScaleMultiplier = new Vector3(10f, 10f, 10f);
					break;
				default:
					m_RegionAreaModel = WResourceManager.LoadObjResource("resources/editor/EditorVerticalPlaneBottomOrigin.obj", new OpenTK.Vector4(1, 1, 1, 1), true, false);
					VisualScaleMultiplier = new Vector3(1f, 1f, 1f);
					break;
			}

			switch (ScaleMode)
			{
				case ScaleModeEnum.Akabe:
					VisualScaleMultiplier = new Vector3(1f, 1f, 1f);
					break;
				case ScaleModeEnum.Akabe10:
					VisualScaleMultiplier = new Vector3(10f, 10f, 1f);
					break;
				case ScaleModeEnum.NBOX:
					VisualScaleMultiplier = new Vector3(1f, 1f, 1f);
					break;
				case ScaleModeEnum.NBOX10:
					VisualScaleMultiplier = new Vector3(10f, 10f, 10f);
					break;
				default:
					VisualScaleMultiplier = new Vector3(1f, 1f, 1f);
					break;
			}
		}

		protected override Vector3 VisualScale
		{
			get
			{
				switch (ScaleMode)
				{
					case ScaleModeEnum.Akabe:
					case ScaleModeEnum.Akabe10:
						return Vector3.Multiply(new Vector3(Transform.LocalScale.X, Transform.LocalScale.Y, 1f), VisualScaleMultiplier);
					case ScaleModeEnum.NBOX:
					case ScaleModeEnum.NBOX10:
						return Vector3.Multiply(Transform.LocalScale, VisualScaleMultiplier);
					default:
						return Vector3.Multiply(Transform.LocalScale, VisualScaleMultiplier);
				}
			}
		}
	}
}
