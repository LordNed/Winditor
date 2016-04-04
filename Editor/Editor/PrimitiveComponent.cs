namespace Editor
{
    class PrimitiveComponent
    {
        public bool RenderStateDirty { get; private set; }

        protected void MarkRenderStateAsDirty()
        {
            RenderStateDirty = true;
        }
    }
}
