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
            if (BehaviorType == BehaviorTypeEnum.Boss_Locked || AppearanceType == AppearanceTypeEnum.Earth_Temple_Boss || AppearanceType == AppearanceTypeEnum.Wind_Temple_Boss)
            {
                model_list.AddRange(WResourceManager.LoadActorResource("Boss Key Lock"));
                isLocked = true;
            }
            else if (BehaviorType == BehaviorTypeEnum.Locked)
            {
                model_list.AddRange(WResourceManager.LoadActorResource("Small Key Lock"));
                isLocked = true;
            }

            VirtualFilesystemDirectory stage_dir = null;
            WDOMNode node = Parent;

            while (!(node is WStage))
            {
                node = node.Parent;
            }

            WStage stage_node = node as WStage;
            stage_dir = stage_node.SourceDirectory;

            if (Switch2 != 255 || (!isLocked && Switch1 != 255))
            {
                if (stage_dir.GetFileAtPath("bdl/stop10.bdl") != null)
                {
                    model_list.Add(WResourceManager.LoadModelFromVFS(stage_dir, "bdl/stop10.bdl"));
                }
            }

            return model_list;
        }
    }
}
