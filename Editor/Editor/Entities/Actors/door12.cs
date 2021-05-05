using JStudio.J3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using WArchiveTools.FileSystem;
using WArchiveTools;
using System.IO;

namespace WindEditor
{
	public partial class door12
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
            m_actorMeshes = GetModelsFromObjectArc();
            if (m_actorMeshes.Count == 0)
                m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new OpenTK.Vector4(1f, 1f, 1f, 1f));
            else
                m_objRender = null;
        }

        private List<J3D> GetModelsFromObjectArc()
        {
            List<J3D> model_list = new List<J3D>();

            switch (AppearanceType)
            {
                case AppearanceTypeEnum.Earth_Temple_Normal:
                    model_list.AddRange(WResourceManager.LoadActorResource("Earth Temple Door"));
                    break;
                case AppearanceTypeEnum.Earth_Temple_Miniboss:
                    model_list.AddRange(WResourceManager.LoadActorResource("Earth Temple Miniboss Door"));
                    break;
                case AppearanceTypeEnum.Earth_Temple_Boss:
                    model_list.AddRange(WResourceManager.LoadActorResource("Earth Temple Boss Door"));
                    break;
                case AppearanceTypeEnum.Wind_Temple_Normal:
                    model_list.AddRange(WResourceManager.LoadActorResource("Wind Temple Door"));
                    break;
                case AppearanceTypeEnum.Wind_Temple_Miniboss:
                    model_list.AddRange(WResourceManager.LoadActorResource("Wind Temple Miniboss Door"));
                    break;
                case AppearanceTypeEnum.Wind_Temple_Boss:
                    model_list.AddRange(WResourceManager.LoadActorResource("Wind Temple Boss Door"));
                    break;
            }

            bool isLocked = false;
            bool isBossLocked = false;
            if (BehaviorType == BehaviorTypeEnum.Boss_locked || AppearanceType == AppearanceTypeEnum.Earth_Temple_Boss || AppearanceType == AppearanceTypeEnum.Wind_Temple_Boss)
            {
                if (FrontSwitch != 255)
                {
                    model_list.AddRange(WResourceManager.LoadActorResource("Boss Key Lock"));
                    isLocked = true;
                    isBossLocked = true;
                }
            }
            else if (BehaviorType == BehaviorTypeEnum.Locked)
            {
                model_list.AddRange(WResourceManager.LoadActorResource("Small Key Lock"));
                isLocked = true;
            }

            WStage stage = World.Map.SceneList.First(x => x.GetType() == typeof(WStage)) as WStage;
            VirtualFilesystemDirectory stage_dir = stage.SourceDirectory;

            bool hasFrontBars = false;
            bool hasBackBars = false;
            if (FrontSwitch != 255 && !isLocked)
                hasFrontBars = true;
            if (BackBarsSwitch != 255)
            {
                hasBackBars = true;
                if (FrontSwitch != 255 && isLocked && !isBossLocked)
                {
                    hasFrontBars = true;
                }
            }
            if (stage_dir.GetFileAtPath("bdl/stop10.bdl") != null)
            {
                if (hasFrontBars)
                {
                    var bars = WResourceManager.LoadModelFromVFS(stage_dir, "bdl/stop10.bdl");
                    model_list.Add(bars);
                }
                if (hasBackBars)
                {
                    var bars = WResourceManager.LoadModelFromVFS(stage_dir, "bdl/stop10.bdl");
                    bars.SetOffsetRotation(new OpenTK.Vector3(0, 180, 0));
                    model_list.Add(bars);
                }
            }

            return model_list;
        }
    }
}
