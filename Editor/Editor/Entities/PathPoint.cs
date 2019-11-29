using JStudio.J3D;
using OpenTK;
using System;
using System.Windows.Data;
using System.Globalization;
using System.ComponentModel;
using WindEditor.ViewModel;

namespace WindEditor.a
{
    public partial class PathPoint_v1
	{
        [WProperty("Path Point", "Name", true)]
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

        [WProperty("Path Point", "Next Point", true)]
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

        public override string ToString()
        {
            return Name;
        }

        override protected void VisibleDOMNode_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //if (e.PropertyName == "NextNode")
            //    PostLoad();
        }

        public override void PostLoad()
        {
            m_objRender = WResourceManager.LoadObjResource("resources/editor/EditorSphere.obj", new OpenTK.Vector4(1, 1, 1, 1));
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
				baseBB += (Transform.Position - NextNode.Transform.Position).Length;
			}

			return baseBB;
		}
		#endregion
	}
}
