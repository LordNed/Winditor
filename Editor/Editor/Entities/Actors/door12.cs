using JStudio.J3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using WArchiveTools.FileSystem;

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
            m_actorMeshes = GetModelsFromStageDir();
            if (m_actorMeshes.Count == 0)
                m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new OpenTK.Vector4(1f, 1f, 1f, 1f));
            else
                m_objRender = null;
        }

        private List<J3D> GetModelsFromStageDir()
        {
            List<J3D> model_list = new List<J3D>();

            VirtualFilesystemDirectory stage_dir = null;
            WDOMNode node = Parent;

            while (!(node is WStage))
            {
                node = node.Parent;
            }

            WStage stage_node = node as WStage;
            stage_dir = stage_node.SourceDirectory;

            if (Type == TypeEnum.Boss)
            {
                if (stage_dir.GetFileAtPath("bdl/door20.bdl") != null)
                {
                    model_list.Add(WResourceManager.LoadModelFromVFS(stage_dir, "bdl/door20.bdl"));
                }
                model_list.AddRange(WResourceManager.LoadActorResource("Boss Key Lock"));

                return model_list;
            }

            if (stage_dir.GetFileAtPath("bdl/door10.bdl") != null)
            {
                model_list.Add(WResourceManager.LoadModelFromVFS(stage_dir, "bdl/door10.bdl"));
            }

            if (Type == TypeEnum.Locked)
            {
                model_list.AddRange(WResourceManager.LoadActorResource("Small Key Lock"));
            }
            else if (Type == TypeEnum.Barred_until_all_enemies_dead || Switch1 < 255)
            {
                model_list.Add(WResourceManager.LoadModelFromVFS(stage_dir, "bdl/stop10.bdl"));
            }

            return model_list;
        }
    }
}
