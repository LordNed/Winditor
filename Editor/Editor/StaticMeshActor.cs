using OpenTK;

namespace WindEditor
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
            Matrix4 modelMatrix = Matrix4.CreateScale(Transform.LocalScale) * Matrix4.CreateFromQuaternion(Transform.Rotation) * Matrix4.CreateTranslation(Transform.Position);
            m_objRenderer.Render(viewMatrix, projMatrix, modelMatrix);
        }

        public override AABox GetAABB()
        {
            AABox modelABB = m_objRenderer.GetAABB();
            return new AABox(modelABB.Min + Transform.Position, modelABB.Max + Transform.Position);
        }
    }
}
