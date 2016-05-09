using OpenTK;

namespace WindEditor
{
    abstract class PrimitiveComponent : WActor, IRenderable
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
