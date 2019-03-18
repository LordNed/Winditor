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
			switch(Name)
            {
                case "Dsaku":
                    m_actorMeshes = WResourceManager.LoadActorResource("Strong Wooden Barrier");
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
