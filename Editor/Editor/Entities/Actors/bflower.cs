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
		public override void PostLoad()
		{
            UpdateModel();
        }

		public override void PreSave()
		{

		}

        private void UpdateModel()
        {
            switch (FlowerType)
            {
                case FlowerTypeEnum.Ripe:
                    m_actorMeshes = WResourceManager.LoadActorResource("Bomb Flower");
                    break;
                case FlowerTypeEnum.Withered:
                    m_actorMeshes = WResourceManager.LoadActorResource("Withered Bomb Flower");
                    break;
                default:
                    m_actorMeshes = WResourceManager.LoadActorResource("Bomb Flower");
                    break;
            }
        }
	}
}
