using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;
using OpenTK;

namespace WindEditor
{
	public partial class andsw0
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
            List<int> usedSwitches = new List<int>();

            usedSwitches.Add(SwitchtoSet);

            int firstSwitch = FirstSwitchtoCheck;
            if (firstSwitch == 0 || firstSwitch == 255)
                firstSwitch = SwitchtoSet + 1;

            for (int i = 0; i < NumSwitchestoCheck; i++)
            {
                usedSwitches.Add(firstSwitch + i);
            }

            usedSwitches.RemoveAll(x => x >= 0xFF || x < 0);
            UsedSwitches = usedSwitches;
        }
    }
}
