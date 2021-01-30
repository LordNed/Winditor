using JStudio.J3D;
using OpenTK;
using System;
using System.Windows.Data;
using System.Globalization;
using System.ComponentModel;
using WindEditor.ViewModel;
using Newtonsoft.Json;

namespace WindEditor
{
    public partial class PathPoint_v2
    {
        public bool IsPathSelected;

        [WProperty("Path Point Properties", "Name", true)]
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

        [JsonIgnore]
        [WProperty("Path Point Properties", "Next Point", true)]
        public PathPoint_v2 NextNode
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

        private PathPoint_v2 m_NextNode;

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
            base.PostLoad();

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

            if (NextNode != null)
            {
                WLinearColor color = WLinearColor.Black;
                if (IsPathSelected)
                    color = WLinearColor.Green;
                m_world.DebugDrawLine(Transform.Position, NextNode.Transform.Position, color, 5f, 0f);
            }

            bool test = this is WDOMNode;
        }

        override public float GetBoundingRadius()
        {
            float baseBB = base.GetBoundingRadius();
            if (NextNode != null)
            {
                baseBB += (Transform.Position- NextNode.Transform.Position).Length;
            }

            return baseBB;
        }
        #endregion
    }
}
