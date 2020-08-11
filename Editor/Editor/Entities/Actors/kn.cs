using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class kn
	{
		public override void PostLoad()
		{
			base.PostLoad();
			m_actorMeshes = WResourceManager.LoadActorResource("Crab");

			// Crabs have a random 50/50 chance of two different sizes.
			if (new Random().NextDouble() >= 0.5f)
			{
				VisualScaleMultiplier = new Vector3(1.5f, 1.5f, 1.5f);
			} else
			{
				VisualScaleMultiplier = new Vector3(2.5f, 2.5f, 2.5f);
			}
		}

		public override void PreSave()
		{

		}
	}
}
