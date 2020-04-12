using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class st
	{
		public override void PostLoad()
        {
            m_actorMeshes = WResourceManager.LoadActorResource("Stalfos");
            base.PostLoad();
		}

		public override void PreSave()
		{

        }

        public override float GetBoundingRadius()
        {
            if (m_actorMeshes.Count == 0)
                return base.GetBoundingRadius();

			var mainModel = m_actorMeshes[0];

			Vector3 lScale = Transform.LocalScale;
			float largestMax = lScale[0];
			for (int i = 1; i < 3; i++)
				if (lScale[i] > largestMax)
					largestMax = lScale[i];

			return largestMax * 160f;
		}
    }
}
