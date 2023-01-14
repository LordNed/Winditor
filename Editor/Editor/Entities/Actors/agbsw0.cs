using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using OpenTK;

namespace WindEditor
{
	public partial class agbsw0
	{
		public override void PostLoad()
		{
			base.PostLoad();

            UpdateModel();
        }

        public override void PreSave()
		{

        }

        public override void AddToRenderer(WSceneView view)
        {
            view.AddTransparentMesh(this);
        }

        private void UpdateModel()
        {
            m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new Vector4(0.5f, 1f, 0f, 1f));
            m_RegionAreaModel = WResourceManager.LoadObjResource("resources/editor/EditorCylinderBottomOrigin.obj", new Vector4(0.5f, 1f, 0f, 1f), true, false);

            switch (BehaviorType)
            {
                case BehaviorTypeEnum.Cursor_or_Timed_Link_Trigger:
                    VisualScaleMultiplier = new Vector3(8000f);
                    break;
                case BehaviorTypeEnum.Item_Restriction_Region:
                    // The code for this one (ExeSubB) is confusing, it looks like it's doing (base*200)-100?
                    // But in practice the regular base*200 multiplier seems to be accurate anyway.
                    VisualScaleMultiplier = new Vector3(200f);
                    break;
                case BehaviorTypeEnum.Tingle_Bomb_Trigger:
                    // Static cylinder with radius 100 and height 100.
                    VisualScaleMultiplier = new Vector3(200f);
                    break;
                case BehaviorTypeEnum.Target_Point:
                    // Just a point, has no region.
                    VisualScaleMultiplier = new Vector3(0f);
                    break;
                default:
                    VisualScaleMultiplier = new Vector3(200f);
                    break;
            }
            // Account for the base size of the OBJ model.
            VisualScaleMultiplier = Vector3.Divide(VisualScaleMultiplier, m_RegionAreaModel.GetAABB().Max);
        }

        protected override Vector3 VisualScale
        {
            get
            {
                switch (BehaviorType)
                {
                    case BehaviorTypeEnum.Tingle_Bomb_Trigger:
                        // Static cylinder , not affected by local scale.
                        return VisualScaleMultiplier;
                    default:
                        return Vector3.Multiply(Transform.LocalScale, VisualScaleMultiplier);
                }
            }
        }

        public override void CalculateUsedSwitches()
        {
            List<int> inSwitches = new List<int>();
            List<int> outSwitches = new List<int>();

            switch (BehaviorType)
            {
                case BehaviorTypeEnum.Repeatable_A_Button_Trigger:
                    inSwitches.Add(RepeatableATriggerConditionSwitch);
                    outSwitches.Add(RepeatableATriggerConditionSwitch);
                    outSwitches.Add(RepeatableATriggerActivatedSwitch);
                    break;
                case BehaviorTypeEnum.Repeatable_Chest_A_Button_Trigger:
                    outSwitches.Add(ChestATriggerActivatedSwitch);
                    break;
                case BehaviorTypeEnum.Marker_A_Button_Trigger:
                    if (MarkerATriggerIcon >= MarkerATriggerIconEnum.Aryll_hint_north_arrow) // M2 or M3
                        break;
                    inSwitches.Add(MarkerATriggerEnabledSwitch);
                    break;
                case BehaviorTypeEnum.OneOff_A_Button_Trigger:
                    inSwitches.Add(OneOffATriggerConditionSwitch);
                    outSwitches.Add(OneOffATriggerConditionSwitch);
                    outSwitches.Add(OneOffATriggerActivatedSwitch);
                    break;
                case BehaviorTypeEnum.Stuck_Cursor_Link_Trigger:
                    inSwitches.Add(LinkStuckCursorConditionSwitch);
                    outSwitches.Add(LinkStuckCursorConditionSwitch);
                    outSwitches.Add(LinkStuckCursorActivatedSwitch);
                    break;
                case BehaviorTypeEnum.Link_Trigger:
                    inSwitches.Add(LinkTriggerConditionSwitch);
                    outSwitches.Add(LinkTriggerConditionSwitch);
                    outSwitches.Add(LinkTriggerActivatedSwitch);
                    break;
                case BehaviorTypeEnum.Tingle_Bomb_Trigger:
                    inSwitches.Add(TingleBombTriggerBombedSwitch);
                    outSwitches.Add(TingleBombTriggerBombedSwitch);
                    break;
                case BehaviorTypeEnum.Target_Point:
                    inSwitches.Add(TargetPointSwitch);
                    if (TargetPointShouldUnsetSwitch)
                        outSwitches.Add(TargetPointSwitch);
                    break;
                case BehaviorTypeEnum.Cursor_or_Timed_Link_Trigger:
                    inSwitches.Add(CursororTimedLinkConditionSwitch);
                    outSwitches.Add(CursororTimedLinkConditionSwitch);
                    outSwitches.Add(CursororTimedLinkActivatedSwitch);
                    break;
                case BehaviorTypeEnum.Secret_Item_A_Button_Trigger:
                    inSwitches.Add(SecretItemSpawnedSwitch);
                    outSwitches.Add(SecretItemSpawnedSwitch);
                    break;
                case BehaviorTypeEnum.Item_Restriction_Region:
                    if (RestrictionType == RestrictionTypeEnum.Tingle_Bombs || RestrictionType == RestrictionTypeEnum.Tingle_Balloon_and_Shield)
                        inSwitches.Add(ItemRestrictionRegionConditionFlag);
                    break;
                case BehaviorTypeEnum.Stuck_Cursor_Secret_Trigger:
                    inSwitches.Add(SecretStuckCursorResetSwitch);
                    inSwitches.Add(SecretStuckCursorActivatedSwitch);
                    outSwitches.Add(SecretStuckCursorActivatedSwitch);
                    break;
                case BehaviorTypeEnum.Link_or_A_Button_Trigger:
                    inSwitches.Add(LinkorATriggerConditionSwitch);
                    outSwitches.Add(LinkorATriggerConditionSwitch);
                    outSwitches.Add(LinkorATriggerActivatedSwitch);
                    break;
            }

            inSwitches.RemoveAll(x => x == 0xFF);
            outSwitches.RemoveAll(x => x == 0xFF);
            UsedInSwitches = inSwitches;
            UsedOutSwitches = outSwitches;
        }
    }
}
