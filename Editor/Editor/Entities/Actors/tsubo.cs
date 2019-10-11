using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

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
            switch (Unknown_5)
            {
                case 0:
                    m_actorMeshes = WResourceManager.LoadActorResource("Small Pot");
                    break;
                case 1:
                    m_actorMeshes = WResourceManager.LoadActorResource("Large Pot");
                    break;
                case 2:
                    m_actorMeshes = WResourceManager.LoadActorResource("Water Pot");
                    break;
                case 3:
                    m_actorMeshes = WResourceManager.LoadActorResource("Barrel 1");
                    break;
                case 4:
                    m_actorMeshes = WResourceManager.LoadActorResource("Stool");
                    break;
                case 5:
                    m_actorMeshes = WResourceManager.LoadActorResource("Skull");
                    break;
                case 6:
                    m_actorMeshes = WResourceManager.LoadActorResource("Bucket");
                    break;
                case 7:
                    m_actorMeshes = WResourceManager.LoadActorResource("Nut");
                    break;
                case 8:
                    m_actorMeshes = WResourceManager.LoadActorResource("Golden Crate");
                    break;
                case 13:
                    m_actorMeshes = WResourceManager.LoadActorResource("Seed");
                    break;
                case 14:
                    m_actorMeshes = WResourceManager.LoadActorResource("Fancy Pot");
                    break;
                case 15:
                    m_actorMeshes = WResourceManager.LoadActorResource("Wooden Crate");
                    break;
            }
        }
	}
}
