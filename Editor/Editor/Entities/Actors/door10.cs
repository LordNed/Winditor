using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using JStudio.J3D;
using WArchiveTools.FileSystem;
using GameFormatReader.Common;

namespace WindEditor
{
	public partial class door10
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
            m_actorMeshes = GetModelsFromStageDir();
            if (m_actorMeshes.Count == 0)
                m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new OpenTK.Vector4(1f, 1f, 1f, 1f));
            else
                m_objRender = null;
        }

        private List<J3D> GetModelsFromStageDir()
        {
            List<J3D> model_list = new List<J3D>();

            WStage stage = World.Map.SceneList.First(x => x.GetType() == typeof(WStage)) as WStage;
            VirtualFilesystemDirectory stage_dir = stage.SourceDirectory;

            if (Type == TypeEnum.Boss)
            {
                if (stage_dir.GetFileAtPath("bdl/door20.bdl") != null)
                {
                    model_list.Add(WResourceManager.LoadModelFromVFS(stage_dir, "bdl/door20.bdl"));
                }
            }
            else
            {
                if (stage_dir.GetFileAtPath("bdl/door10.bdl") != null)
                {
                    model_list.Add(WResourceManager.LoadModelFromVFS(stage_dir, "bdl/door10.bdl"));
                }
            }

            bool isLocked = false;
            bool isBossLocked = false;
            if (Type == TypeEnum.Boss)
            {
                if (FrontSwitch != 255)
                {
                    model_list.AddRange(WResourceManager.LoadActorResource("Boss Key Lock"));
                    isLocked = true;
                    isBossLocked = true;
                }
            }
            else if (Type == TypeEnum.Locked || Type == TypeEnum.Locked_and_barred)
            {
                model_list.AddRange(WResourceManager.LoadActorResource("Small Key Lock"));
                isLocked = true;
            }

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
