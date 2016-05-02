using OpenTK;

namespace Editor
{
    class WStaticMeshActor : WActor, IRenderable
    {
        private SimpleObjRenderer m_objRenderer;

        public WStaticMeshActor(string filePath)
        {
            Obj obj = new Obj();
            obj.Load(filePath);
            m_objRenderer = new SimpleObjRenderer(obj);
        }

        public override void Tick(float deltaTime)
        {
        }

        public void ReleaseResources()
        {
            m_objRenderer.ReleaseResources();
        }

        public void Render(Matrix4 viewMatrix, Matrix4 projMatrix)
        {
            Matrix4 modelMatrix = 

            m_objRenderer.Render(viewMatrix, projMatrix, modelMatrix);
        }


    }
}
