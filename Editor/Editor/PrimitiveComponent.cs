using OpenTK;

namespace Editor
{
    abstract class PrimitiveComponent : IRenderable, ITickableObject
    {
        public bool RenderStateDirty { get; protected set; }

        public abstract void ReleaseResources();
        public abstract void Render(Matrix4 viewMatrix, Matrix4 projMatrix);
        public abstract void Tick(float deltaTime);

        protected void MarkRenderStateAsDirty()
        {
            RenderStateDirty = true;
        }
    }
}
