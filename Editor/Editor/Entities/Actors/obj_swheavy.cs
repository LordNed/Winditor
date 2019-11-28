using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using OpenTK;

namespace WindEditor
{
	public partial class obj_swheavy
	{
		public override void PostLoad()
        {
            Transform.LocalScale = new Vector3(1.5f, 1f, 1.5f);
            m_actorMeshes = WResourceManager.LoadActorResource("Heavy Button Switch");
            base.PostLoad();
		}

		public override void PreSave()
		{

		}
	}
}
