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
        [WProperty("Door", "Door Type", true, "The state of the door when it spawns.")]
        public DoorType DoorType
        {
            get { return (DoorType)Type; }
            set
            {
                if ((int)value != Type)
                {
                    Type = (int)value;
                    OnPropertyChanged("DoorType");
                }
            }
        }

		public override void PostLoad()
		{
            m_actorMeshes = GetModelFromStageDir();
		}

		public override void PreSave()
		{

		}

        private List<J3D> GetModelFromStageDir()
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

            if (DoorType == DoorType.Boss)
            {
                model_list.Add(WResourceManager.LoadModelFromVFS(stage_dir, "bdl/door20.bdl"));
                model_list.AddRange(WResourceManager.LoadActorResource("Boss Key Lock"));

                return model_list;
            }

            model_list.Add(WResourceManager.LoadModelFromVFS(stage_dir, "bdl/door10.bdl"));

            if (DoorType == DoorType.Locked)
            {
                model_list.AddRange(WResourceManager.LoadActorResource("Small Key Lock"));
            }
            else if (DoorType == DoorType.Barred || SwitchBit < 255)
            {
                model_list.Add(WResourceManager.LoadModelFromVFS(stage_dir, "bdl/stop10.bdl"));
            }

            return model_list;
        }
	}
}
