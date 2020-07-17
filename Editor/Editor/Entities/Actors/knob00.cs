using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using WindEditor.Minitors;

namespace WindEditor
{
	public partial class knob00
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
			switch (Style)
			{
				case StyleEnum.Outset:
					m_actorMeshes = WResourceManager.LoadActorResource("Outset Door");
					break;
				case StyleEnum.Pirate_Ship:
					m_actorMeshes = WResourceManager.LoadActorResource("Pirate Ship Door");
					break;
				case StyleEnum.Windfall:
					m_actorMeshes = WResourceManager.LoadActorResource("Windfall Door");
					break;
				case StyleEnum.Rito_Aerie:
					m_actorMeshes = WResourceManager.LoadActorResource("Rito Aerie Door");
					break;
				case StyleEnum.Private_Oasis:
					m_actorMeshes = WResourceManager.LoadActorResource("Private Oasis Door");
					break;
				case StyleEnum.Forsaken_Fortress:
					m_actorMeshes = WResourceManager.LoadActorResource("Forsaken Fortress Door");
					break;
				case StyleEnum.Nintendo_Gallery:
					m_actorMeshes = WResourceManager.LoadActorResource("Nintendo Gallery Door");
					break;
				case StyleEnum.Fancy:
					m_actorMeshes = WResourceManager.LoadActorResource("Fancy Door");
					break;
				default:
					m_actorMeshes = WResourceManager.LoadActorResource("Outset Door");
					break;
			}
		}
	}
}
