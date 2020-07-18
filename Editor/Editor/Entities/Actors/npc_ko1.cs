using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class npc_ko1
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
			if (Name == "Ko1")
			{
				switch (ZillType)
				{
					case ZillTypeEnum.Chasing_Zill:
					case ZillTypeEnum.Standing_Zill:
						m_actorMeshes = WResourceManager.LoadActorResource("Zill");
						break;
					case ZillTypeEnum.Sleeping_Zill:
						m_actorMeshes = WResourceManager.LoadActorResource("Sleeping Zill");
						break;
					default:
						m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new OpenTK.Vector4(1f, 1f, 1f, 1f));
						break;
				}
			} else if (Name == "Ko2")
			{
				switch (JoelType)
				{
					case JoelTypeEnum.Joel_with_Stick:
						m_actorMeshes = WResourceManager.LoadActorResource("Joel with Stick");
						break;
					case JoelTypeEnum.Standing_Joel:
						m_actorMeshes = WResourceManager.LoadActorResource("Joel");
						break;
					case JoelTypeEnum.Sleeping_Joel:
						m_actorMeshes = WResourceManager.LoadActorResource("Sleeping Joel");
						break;
					default:
						m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new OpenTK.Vector4(1f, 1f, 1f, 1f));
						break;
				}
			} else
			{
				m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new OpenTK.Vector4(1f, 1f, 1f, 1f));
			}
		}
	}
}
