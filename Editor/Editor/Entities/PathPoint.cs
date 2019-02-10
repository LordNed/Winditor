using JStudio.J3D;
using OpenTK;
using System;
using System.Windows.Data;
using System.Globalization;

namespace WindEditor
{
    public partial class PathPoint_v1
	{
		public Path_v1 NextNode { get; set; }
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
