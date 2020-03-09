using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using OpenTK;

namespace WindEditor
{
	public partial class tsubo
	{
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
            Transform.LocalScale = new Vector3(1f, 1f, 1f);
            switch (Type)
            {
                case TypeEnum.Small_pot:
                    m_actorMeshes = WResourceManager.LoadActorResource("Small Pot");
                    break;
                case TypeEnum.Large_pot:
                    m_actorMeshes = WResourceManager.LoadActorResource("Large Pot");
                    break;
                case TypeEnum.Water_pot:
                    m_actorMeshes = WResourceManager.LoadActorResource("Water Pot");
                    break;
                case TypeEnum.Barrel:
                    m_actorMeshes = WResourceManager.LoadActorResource("Barrel 1");
                    break;
                case TypeEnum.Stool:
                    m_actorMeshes = WResourceManager.LoadActorResource("Stool");
                    break;
                case TypeEnum.Skull:
                    m_actorMeshes = WResourceManager.LoadActorResource("Skull");
                    break;
                case TypeEnum.Bucket:
                    m_actorMeshes = WResourceManager.LoadActorResource("Bucket");
                    break;
                case TypeEnum.Nut:
                    m_actorMeshes = WResourceManager.LoadActorResource("Nut");
                    break;
                case TypeEnum.Golden_crate:
                    Transform.LocalScale = new Vector3(0.4f, 0.4f, 0.4f);
                    m_actorMeshes = WResourceManager.LoadActorResource("Golden Crate");
                    break;
                case TypeEnum.Seed:
                    m_actorMeshes = WResourceManager.LoadActorResource("Seed");
                    break;
                case TypeEnum.Fancy_pot:
                    m_actorMeshes = WResourceManager.LoadActorResource("Fancy Pot");
                    break;
                case TypeEnum.Wooden_crate:
                    Transform.LocalScale = new Vector3(0.5f, 0.5f, 0.5f);
                    m_actorMeshes = WResourceManager.LoadActorResource("Wooden Crate");
                    break;
                default:
                    m_actorMeshes = WResourceManager.LoadActorResource("Blue Tower of the Gods Pillar Statue");
                    break;
            }
        }
	}
}
