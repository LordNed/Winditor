using JStudio.J3D;
using OpenTK;

namespace WindEditor
{
    public class J3DNode : WDOMNode, IRenderable
    {
        public J3D Model
        {
            get { return m_model; }
            set
            {
                if (m_model != value)
                {
                    m_model = value;
                    OnPropertyChanged("Model");
                }
            }
        }

        public string Filename
        {
            get { return m_filename; }
            set
            {
                if (m_filename != value)
                {
                    m_filename = value;
                    OnPropertyChanged("Filename");
                }
            }
        }

        private J3D m_model;
        private string m_filename;

        public J3DNode(J3D model, WWorld world, string filename = "") : base(world)
        {
            m_model = model;
            Name = model.Name;
            Filename = filename;

            IsRendered = true;
        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);

            m_model.Tick(deltaTime);
        }

        public override string ToString()
        {
            return m_model.Name;
        }

        #region IRenderable
        void IRenderable.AddToRenderer(WSceneView view)
        {
            view.AddOpaqueMesh(this);
        }

        void IRenderable.Draw(WSceneView view)
        {
            Matrix4 trs = Matrix4.CreateScale(Transform.LocalScale) * Matrix4.CreateFromQuaternion(Transform.Rotation.ToSinglePrecision()) * Matrix4.CreateTranslation(Transform.Position);
            m_model.Render(view.ViewMatrix, view.ProjMatrix, trs);
        }

        Vector3 IRenderable.GetPosition()
        {
            return Transform.Position;
        }

        float IRenderable.GetBoundingRadius()
        {
            return float.MaxValue;
        }
        #endregion
    }
}
