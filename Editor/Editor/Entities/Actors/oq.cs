using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using OpenTK;

namespace WindEditor
{
	public partial class oq
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
			VisualScaleMultiplier = Vector3.One;
			switch (Type)
			{
				case TypeEnum.Freshwater_Octorok:
					m_actorMeshes = WResourceManager.LoadActorResource("Freshwater Octorok");
					break;
				case TypeEnum.Rock_shot_by_a_freshwater_Octorok:
					m_actorMeshes = WResourceManager.LoadActorResource("Freshwater Octorok Rock");
					break;
				default:
					VisualScaleMultiplier = new Vector3(2.75f, 2.75f, 2.75f);
					m_actorMeshes = WResourceManager.LoadActorResource("Saltwater Octorok");
					break;
			}
		}
	}
}
