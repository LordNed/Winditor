using JStudio.J3D;
using OpenTK;
using System;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class TreasureChest
	{
		public override string ToString()
		{
			return Name;
		}

        public override void PostLoad()
        {
            UpdateModel();
            base.PostLoad();
        }

        private void UpdateModel()
        {
            switch(AppearanceType)
            {
                case AppearanceTypeEnum.Big_Key:
                    m_actorMeshes = WResourceManager.LoadActorResource("Big Key Chest");
                    break;
                case AppearanceTypeEnum.Metal:
                    m_actorMeshes = WResourceManager.LoadActorResource("Metal Chest");
                    break;
                case AppearanceTypeEnum.Dark_wood:
                    m_actorMeshes = WResourceManager.LoadActorResource("Dark Wood Chest");
                    break;
                case AppearanceTypeEnum.Light_wood:
                default:
                    m_actorMeshes = WResourceManager.LoadActorResource("Light Wood Chest");
                    break;
            }
        }

        public override void PopulateDefaultProperties()
        {
            Name = "takara";
            EnemyNumber = -1;
            AppearConditionSwitch = -1;
            ChestOpenFlag = -1;
            OpenSwitch = -1;

            // Try to assign the room index automatically if this is a room chest.
            WDOMNode cur_object = this;
            while (cur_object.Parent != null)
            {
                cur_object = cur_object.Parent;
            }
            WScene scene = cur_object as WScene;
            if (scene is WRoom)
            {
                WRoom room = scene as WRoom;
                RoomNumber = room.RoomIndex;
            }
        }
    }
}
