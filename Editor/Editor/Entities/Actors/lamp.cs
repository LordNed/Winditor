using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using OpenTK;

namespace WindEditor
{
	public partial class lamp
	{
		public override void PostLoad()
        {
            VisualScaleMultiplier = new Vector3(0.4f, 0.4f, 0.4f);
            m_actorMeshes = WResourceManager.LoadActorResource("Lamp");
            base.PostLoad();
		}

		public override void PreSave()
		{

		}
	}
}
