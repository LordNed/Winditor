using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class wind_tag
	{
		public override void PostLoad()
		{
			// This is the correct mesh, but it renders invisibly. So just use debug cubes for now.
			//m_actorMeshes = WResourceManager.LoadActorResource("Wind Column");
			base.PostLoad();
		}

		public override void PreSave()
		{

		}
	}
}
