using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class bflower
	{
        [WProperty("Bomb Flower", "Flower State", true, "Whether the flower spawns as ripe or withered.")]
        public BombFlowerType Type
        {
            get { return (BombFlowerType)FlowerType; }
            set
            {
                if (FlowerType != (int)value)
                {
                    FlowerType = (int)value;
                    OnPropertyChanged("Type");
                }
            }
        }
		public override void PostLoad()
		{
			switch (Type)
            {
                case BombFlowerType.Ripe:
                    m_actorMeshes = WResourceManager.LoadActorResource("Bomb Flower");
                    break;
                case BombFlowerType.Withered:
                    m_actorMeshes = WResourceManager.LoadActorResource("Withered Bomb Flower");
                    break;
                default:
                    base.PostLoad();
                    break;
            }
		}

		public override void PreSave()
		{

		}
	}
}
