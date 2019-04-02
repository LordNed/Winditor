using JStudio.J3D;
using OpenTK;
using System;

namespace WindEditor
{
	public partial class SpawnPoint
	{
		public override string ToString()
		{
			return Name;
		}

        public override void PostLoad()
        {
            if (string.IsNullOrEmpty(Name))
                Name = "Link";

            m_actorMeshes = WResourceManager.LoadActorResource("Link");
        }
    }
}
