using JStudio.J3D;
using OpenTK;

namespace WindEditor
{
    public class J3DNode : WDOMNode, IRenderable
    {
        public J3D Model { get { return m_model; } }

        private J3D m_model;

        public J3DNode(J3D model, WWorld world) : base(world)
        {
            m_model = model;
        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);

            m_model.Tick(deltaTime);
        }

        #region IRenderable
        void IRenderable.AddToRenderer(WSceneView view)
        {
            view.AddOpaqueMesh(this);
        }

        void IRenderable.Draw(WSceneView view)
        {
            Matrix4 trs = Matrix4.CreateScale(Transform.LocalScale) * Matrix4.CreateFromQuaternion(Transform.Rotation) * Matrix4.CreateTranslation(Transform.Position);
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
