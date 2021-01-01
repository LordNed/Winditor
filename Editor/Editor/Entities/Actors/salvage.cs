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
			List<int> usedSwitches = new List<int>();

			if (Type == TypeEnum.Checks_Switch)
			{
				usedSwitches.Add(SwitchtoCheck);
			}

			usedSwitches.RemoveAll(x => x == 0xFF);
			UsedSwitches = usedSwitches;
		}
	}
}
