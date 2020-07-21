using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class wz
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
			switch (BehaviorType)
			{
				case BehaviorTypeEnum.Shoots_Fireballs:
					m_actorMeshes = WResourceManager.LoadActorResource("Fireball-Shooting Wizzrobe");
					break;
				case BehaviorTypeEnum.Spawns_Enemies_and_Shoots_Fireballs:
					m_actorMeshes = WResourceManager.LoadActorResource("Spawner Wizzrobe");
					break;
				case BehaviorTypeEnum.Miniboss:
					m_actorMeshes = WResourceManager.LoadActorResource("Mini-boss Wizzrobe");
					break;
				case BehaviorTypeEnum.Shoots_Fireballs_Alt_Color:
					m_actorMeshes = WResourceManager.LoadActorResource("Alt Color Fireball Wizzrobe");
					break;
				default:
					m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new OpenTK.Vector4(1f, 1f, 1f, 1f));
					break;
			}
		}
	}
}
