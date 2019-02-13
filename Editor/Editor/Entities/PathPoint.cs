using JStudio.J3D;
using OpenTK;
using System;
using System.Windows.Data;
using System.Globalization;
using System.ComponentModel;
using WindEditor.ViewModel;

namespace WindEditor
{
    public partial class PathPoint_v1
	{
        [WProperty("Path Point Properties", "Next Point", true)]
		public PathPoint_v1 NextNode
        {
            get { return m_NextNode; }
            set
            {
                if (value != m_NextNode)
                {
                    m_NextNode = value;
                    OnPropertyChanged("NextNode");
                }
            }
        }

        private PathPoint_v1 m_NextNode;

        override protected void VisibleDOMNode_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //if (e.PropertyName == "NextNode")
            //    PostLoad();
        }

		#region IRenderable
		override public void AddToRenderer(WSceneView view)
		{
			view.AddOpaqueMesh(this);
		}

		override public void Draw(WSceneView view)
		{
			base.Draw(view);

			if(NextNode != null)
			{
				m_world.DebugDrawLine(Transform.Position, NextNode.Transform.Position, WLinearColor.Black, 5f, 0f);
			}
		}

		override public float GetBoundingRadius()
		{
			float baseBB = base.GetBoundingRadius();
			if(NextNode != null)
			{
				baseBB += (Transform.Position.BackingVector - NextNode.Transform.Position.BackingVector).Length;
			}

			return baseBB;
		}
		#endregion
	}
}
