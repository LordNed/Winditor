using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using OpenTK;

namespace WindEditor
{
	public partial class andsw2
	{
		public override void PostLoad()
        {
            m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorCube.obj", new Vector4(0, 1, 0, 1), true);
		}

		public override void PreSave()
		{

        }

        public override void AddToRenderer(WSceneView view)
        {
            view.AddTransparentMesh(this);
        }

        public override void CalculateUsedSwitches()
        {
            List<int> inSwitches = new List<int>();
            List<int> outSwitches = new List<int>();

            outSwitches.Add(SwitchtoSet);

            if (FirstSwitchtoCheck == 255 || NumSwitchestoCheck == 255)
            {
                UsedInSwitches = inSwitches;
                UsedOutSwitches = outSwitches;
                return;
            }

            for (int i = 0; i < NumSwitchestoCheck; i++)
            {
                inSwitches.Add(FirstSwitchtoCheck + i);
            }

            inSwitches.RemoveAll(x => x >= 0xFF || x < 0);
            outSwitches.RemoveAll(x => x >= 0xFF || x < 0);
            UsedInSwitches = inSwitches;
            UsedOutSwitches = outSwitches;
        }
    }
}
