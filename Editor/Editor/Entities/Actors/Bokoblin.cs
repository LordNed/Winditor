using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor
{
    public partial class bk
    {
        [WProperty("Bokoblin", "Type", true, "The behavior of the Bokoblin when it spawns.")]
        public BokoblinType Type
        {
            get { return (BokoblinType)Unknown_1; }
            set
            {
                if ((int)value != Unknown_1)
                {
                    Unknown_1 = (int)value;
                    OnPropertyChanged("Type");

                    UpdateModel();
                }
            }
        }

        [WProperty("Bokoblin", "Switch Spawns Bokoblin", true, "If this is set, the switch ID below is for spawning the Bokoblin, rather than a switch to set when it's killed.")]
        public bool SwitchSpawnsBokoblin
        {
            get { return Convert.ToBoolean(Unknown_2); }
            set
            {
                int bool_as_int = Convert.ToInt32(value);

                if (bool_as_int != Unknown_2)
                {
                    Unknown_2 = bool_as_int;
                    OnPropertyChanged("SwitchSpawnsBokoblin");
                }
            }
        }

        [WProperty("Bokoblin", "Is Green", true, "If this is set, the Bokoblin is green. However, this is overriden by the Pink Bokoblin with Telescope type.")]
        public bool IsMiniboss
        {
            get { return Convert.ToBoolean(Unknown_3); }
            set
            {
                int bool_as_int = Convert.ToInt32(value);

                if (bool_as_int != Unknown_3)
                {
                    Unknown_3 = bool_as_int;
                    OnPropertyChanged("IsMiniboss");

                    UpdateModel();
                }
            }
        }

        [WProperty("Bokoblin", "Weapon", true, "The weapon that the Bokoblin is holding when it spawns.")]
        public BokoblinHeldItem Weapon
        {
            get { return (BokoblinHeldItem)Unknown_4; }
            set
            {
                if ((int)value != Unknown_4)
                {
                    Unknown_4 = (int)value;
                    OnPropertyChanged("Weapon");
                }
            }
        }

        private Path_v2 m_Path;

        [WProperty("Bokoblin", "Path", true, "The path that the Bokoblin follows if it")]
        public Path_v2 Path
        {
            get { return m_Path; }
            set
            {
                if (value != m_Path)
                {
                    m_Path = value;
                    OnPropertyChanged("Path");
                }
            }
        }

        public override void PostLoad()
        {
            UpdateModel();
        }

        private void UpdateModel()
        {
            m_actorMeshes = WResourceManager.LoadActorResource("Blue Bokoblin");

            if (IsMiniboss)
            {
                m_actorMeshes = WResourceManager.LoadActorResource("Green Bokoblin");
            }
            
            if (Type == BokoblinType.Pink_Bokoblin_with_Telescope)
            {
                m_actorMeshes = WResourceManager.LoadActorResource("Pink Bokoblin");
            }
        }
    }
}
