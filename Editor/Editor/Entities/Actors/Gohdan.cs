using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor
{
    public partial class bst
    {
        [WProperty("Gohdan", "Component ID", true)]
        public GohdanComponent ComponentID
        {
            get { return (GohdanComponent)ComponentType; }
            set
            {
                if ((int)value != ComponentType)
                {
                    ComponentType = (int)value;
                    OnPropertyChanged("ComponentID");

                    UpdateModel();
                }
            }
        }

        private void UpdateModel()
        {
            switch (ComponentID)
            {
                case GohdanComponent.Head:
                    m_actorMeshes = WResourceManager.LoadActorResource("Gohdan Head");
                    break;
                case GohdanComponent.Left_Hand:
                    m_actorMeshes = WResourceManager.LoadActorResource("Gohdan Left Hand");
                    break;
                case GohdanComponent.Right_Hand:
                    m_actorMeshes = WResourceManager.LoadActorResource("Gohdan Right Hand");
                    break;
            }
        }

        public override void PostLoad()
        {
            UpdateModel();
        }
    }
}
