using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class bita
    {
        public override void PostLoad()
        {
            UpdateModel();
        }

        private void UpdateModel()
        {
            if (Model == ModelEnum.Jagged_Edge)
            {
                m_actorMeshes = WResourceManager.LoadActorResource("Jagged Edge Wooden Platform");
            } else
            {
                m_actorMeshes = WResourceManager.LoadActorResource("Straight Edge Wooden Platform");
            }
        }
    }
}
