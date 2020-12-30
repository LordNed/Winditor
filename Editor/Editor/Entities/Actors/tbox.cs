using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
    public partial class tbox
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
            switch (AppearanceType)
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

        public override void CalculateUsedSwitches()
        {
            List<int> usedSwitches = new List<int>();

            usedSwitches.Add(OpenSwitch);

            switch (BehaviorType)
            {
                case BehaviorTypeEnum.Spawn_when_a_switch_is_set:
                case BehaviorTypeEnum.Visible_but_unopenable_until_a_switch_is_set:
                case BehaviorTypeEnum.Transparent_until_a_switch_is_set:
                case BehaviorTypeEnum.Spawn_on_Triforce_emblem_when_a_switch_is_set:
                case BehaviorTypeEnum.Uses_Stage_Save_Info_1_open_flag_and_spawns_when_a_switch_is_set:
                    usedSwitches.Add(AppearConditionSwitch);
                    break;
                case BehaviorTypeEnum.Spawn_when_all_enemies_dead:
                    // Note: This type sets the switch instead of just checking it.
                    usedSwitches.Add(AppearConditionSwitch);
                    break;
            }

            usedSwitches.RemoveAll(x => x == 0xFF);
            UsedSwitches = usedSwitches;
        }

        public override int GetRoomNum()
        {
            return RoomNumber;
        }
    }
}
