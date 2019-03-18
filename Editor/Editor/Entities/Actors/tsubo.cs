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
			switch(Name)
            {
                case "Kmi00":
                    m_actorMeshes = WResourceManager.LoadActorResource("Nut");
                    break;
                default:
                    base.PostLoad();
                    break;
            }
		}

		public override void PreSave()
		{

		}
	}
}
