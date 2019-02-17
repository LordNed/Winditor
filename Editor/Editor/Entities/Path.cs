using JStudio.J3D;
using OpenTK;
using System;
using System.Windows.Data;
using System.Globalization;
using System.Collections.Generic;
using WindEditor.ViewModel;

namespace WindEditor
{
	public partial class Path_v1
	{
        [WProperty("Path", "Name", true)]
        override public string Name
        {
            get { return m_Name; }
            set
            {
                m_Name = value;
                OnPropertyChanged("Name");
            }
        }

        private string m_Name;

        [WProperty("Path Properties", "First Point", true)]
        public PathPoint_v1 FirstNode
        {
            get { return m_FirstNode; }
            set
            {
                if (value != m_FirstNode)
                {
                    m_FirstNode = value;
                    OnPropertyChanged("FirstNode");
                }
            }
        }

        private PathPoint_v1 m_FirstNode;

        public override string ToString()
        {
            return Name;
        }

        public void SetNodes(List<WDOMNode> points)
        {
            int first_index = m_FirstEntryOffset / 16;

            FirstNode = (PathPoint_v1)points[first_index];
            FirstNode.Name = Name + $"_{0}";

            PathPoint_v1 cur_node = FirstNode;

            for (int i = 1; i < m_NumberofPoints; i++)
            {
                int next_index = first_index + i;
                cur_node.NextNode = (PathPoint_v1)points[next_index];
                cur_node.NextNode.Name = Name + $"_{i}";
                cur_node = cur_node.NextNode;
            }
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
