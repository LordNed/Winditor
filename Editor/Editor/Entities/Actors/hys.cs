using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using OpenTK;

namespace WindEditor
{
	public partial class hys
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
			m_actorMeshes = WResourceManager.LoadActorResource("Shootable Eye Target");

			switch (Size)
            {
				case SizeEnum.Large:
					VisualScaleMultiplier = new Vector3(2);
					break;
				default:
					VisualScaleMultiplier = new Vector3(1);
					break;
			}
		}
	}
}
