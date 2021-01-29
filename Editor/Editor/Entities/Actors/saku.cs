using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class saku
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
			if (Strength == StrengthEnum.Weak_Boarded_Up_Wall)
			{
				if (Height == HeightEnum.Single)
					m_actorMeshes = WResourceManager.LoadActorResource("Weak Wooden Barrier 1x");
				else
					m_actorMeshes = WResourceManager.LoadActorResource("Weak Wooden Barrier 2x");
			} else
			{
				if (Height == HeightEnum.Single)
					m_actorMeshes = WResourceManager.LoadActorResource("Strong Wooden Barrier 1x");
				else
					m_actorMeshes = WResourceManager.LoadActorResource("Strong Wooden Barrier 2x");
			}
		}
	}
}
