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

            if (Name == "pflower" || Name.StartsWith("pflwr"))
            {
                m_objRender = WResourceManager.LoadObjResource("resources/models/flowers/pflower_pink.obj");
            }
            else if (Name == "flower" || Name.StartsWith("flwr"))
            {
                m_objRender = WResourceManager.LoadObjResource("resources/models/flowers/flower.obj");
            }
        }
    }
}
