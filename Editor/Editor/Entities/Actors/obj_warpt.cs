using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class obj_warpt
	{
		public override void PostLoad()
		{
            m_actorMeshes = WResourceManager.LoadActorResource("Warp Pot");
            base.PostLoad();
		}

		public override void PreSave()
		{

		}

		public override void CalculateUsedSwitches()
		{
			List<int> inSwitches = new List<int>();
			List<int> outSwitches = new List<int>();

			if (Type == TypeEnum.First_in_cycle || Type == TypeEnum.Second_in_cycle || Type == TypeEnum.Third_in_cycle)
			{
				// Cyclic
				inSwitches.Add(TopUnblockedSwitch);
			} else
			{
				// Noncyclic
				outSwitches.Add(ThisUnlockedSwitch);
				inSwitches.Add(DestinationUnlockedSwitch);
			}

			inSwitches.RemoveAll(x => x == 0xFF);
			outSwitches.RemoveAll(x => x == 0xFF);
			UsedInSwitches = inSwitches;
			UsedOutSwitches = outSwitches;
		}
	}
}
