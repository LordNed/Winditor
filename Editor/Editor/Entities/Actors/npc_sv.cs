using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class npc_sv
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
            m_actorMeshes.Clear();
            m_objRender = null;

            if (Name == "Sv0")
                m_actorMeshes = WResourceManager.LoadActorResource("Standing Diving Man");
            else
                m_actorMeshes = WResourceManager.LoadActorResource("Sitting Diving Man");
        }
    }
}
