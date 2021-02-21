using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class hmlif
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
			switch (Type)
			{
				case TypeEnum.No_Eye:
				default:
					m_actorMeshes = WResourceManager.LoadActorResource("Moving Platform");
					break;
				case TypeEnum.Eye_on_Top:
					m_actorMeshes = WResourceManager.LoadActorResource("Moving Platform With Eye on Top");
					break;
				case TypeEnum.Eye_on_Bottom:
					m_actorMeshes = WResourceManager.LoadActorResource("Moving Platform With Eye on Bottom");
					break;
			}
		}
	}
}
