using JStudio.J3D;

namespace WindEditor
{
    public class J3DDOMNode : WDOMNode
    {
        private J3D m_model;

        public J3DDOMNode(J3D model)
        {
            m_model = model;
        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);

            m_model.Tick(deltaTime);
        }

        public override void Render()
        {
            base.Render();

            //m_model.
        }
    }
}
