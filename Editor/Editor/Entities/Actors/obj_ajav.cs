using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class obj_ajav
	{
		public override void PostLoad()
		{
			m_actorMeshes = WResourceManager.LoadActorResource("Jabun's Bombable Stone Wall");
			// TODO: Models 1-5 need to have their textures copied from model 0 to look correct.
			// Winditor's code doesn't seem to support copying textures between models without crashing.
			base.PostLoad();
		}

		public override void PreSave()
		{

		}
	}
}
