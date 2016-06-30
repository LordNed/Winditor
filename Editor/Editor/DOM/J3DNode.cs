using JStudio.J3D;
using OpenTK;

namespace WindEditor
{
    public class J3DNode : WDOMNode
    {
        public J3D Model { get { return m_model; } }

        private J3D m_model;

        public J3DNode(J3D model)
        {
            m_model = model;
        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);

            m_model.Tick(deltaTime);
        }

        public override void Render(WSceneView view)
        {
            base.Render(view);

            m_model.Render(view.ViewMatrix, view.ProjMatrix, Matrix4.Identity);
        }
    }
}
