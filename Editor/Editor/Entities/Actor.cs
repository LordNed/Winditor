using JStudio.J3D;
using OpenTK;
using System;

namespace WindEditor
{
	public partial class Actor
	{
		public override string ToString()
		{
			return Name;
		}

        public override void PostLoad()
        {
            base.PostLoad();
        }

        public override void PopulateDefaultProperties()
        {
            base.PopulateDefaultProperties();
            EnemyNumber = -1;
        }
    }
}
