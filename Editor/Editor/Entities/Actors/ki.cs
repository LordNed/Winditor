using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class ki
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
            if (IsFireKeese)
            {
                if (BehaviorType == BehaviorTypeEnum.Hanging_from_ceiling || BehaviorType == BehaviorTypeEnum.Hanging_from_ceiling_passive)
                {
                    m_actorMeshes = WResourceManager.LoadActorResource("Hanging Fire Keese");
                }
                else
                {
                    m_actorMeshes = WResourceManager.LoadActorResource("Flying Fire Keese");
                }
            }
            else
            {
                if (BehaviorType == BehaviorTypeEnum.Hanging_from_ceiling || BehaviorType == BehaviorTypeEnum.Hanging_from_ceiling_passive)
                {
                    m_actorMeshes = WResourceManager.LoadActorResource("Hanging Keese");
                }
                else
                {
                    m_actorMeshes = WResourceManager.LoadActorResource("Flying Keese");
                }
            }
        }
	}
}
