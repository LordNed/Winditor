using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class obj_hsehi1
    {
        // Name affects the type.
        [WProperty("Misc.", "Name", true)]
        override public string Name
        {
            get { return m_Name; }
            set
            {
                m_Name = value;
                OnPropertyChanged("Name");
                UpdateModel();
            }
        }

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
            if (Name == "Hsh")
            {
                m_actorMeshes = WResourceManager.LoadActorResource("Command Melody Stone");
            }
            else
            {
                m_actorMeshes = WResourceManager.LoadActorResource("Tower of the Gods Instruction Stone");
            }
        }
	}
}
