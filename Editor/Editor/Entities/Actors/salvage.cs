using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class salvage
	{
		public override void PostLoad()
		{
			base.PostLoad();
		}

		public override void PreSave()
		{

		}

		public override void CalculateUsedSwitches()
		{
			List<int> inSwitches = new List<int>();
			List<int> outSwitches = new List<int>();

			if (Type == TypeEnum.Checks_Switch)
			{
				inSwitches.Add(SwitchtoCheck);
			}

			inSwitches.RemoveAll(x => x == 0xFF);
			outSwitches.RemoveAll(x => x == 0xFF);
			UsedInSwitches = inSwitches;
			UsedOutSwitches = outSwitches;
		}
	}
}
