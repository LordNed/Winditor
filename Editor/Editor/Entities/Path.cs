using JStudio.J3D;
using OpenTK;
using System;
using System.Windows.Data;
using System.Globalization;

namespace WindEditor
{
	public partial class Path_v1
	{
		public Path_v1 FirstNode { get; set; }

		public override void PostLoad()
		{
			base.PostLoad();

			// ToDo: Get the index of our first node, into the array of passed in entities.
			// assign that as our FirstNode, and then recursively walk the children assigning their next node,
			// etc. until we hit a end-of-path node. 
		}

		// override void PreSave(...)
		// {
		//		int NodeIndex = InList.GetNodesOfType<PathPoint_v1>().IndexOf(FirstNode);
		//		if(NodeIndex< 0)
		// 		{
		// 			Console.WriteLine("Warning blahblah null setting to zero to try and keep the game from crashing on load.");
		// 			NodeIndex = 0;
		// 		}
		// 
		//		// Set the property for FirstIndex to NodeIndex, etc etc.
		// 
		// }
	}
}
