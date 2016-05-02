using OpenTK;

namespace Editor
{
    abstract class PrimitiveComponent : WActor
    {
        public bool RenderStateDirty { get; protected set; }

        public abstract void ReleaseResources();
        public abstract void Render(Matrix4 viewMatrix, Matrix4 projMatrix);

        protected void MarkRenderStateAsDirty()
        {
            RenderStateDirty = true;
        }
    }
}
