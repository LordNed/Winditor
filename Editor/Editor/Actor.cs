namespace Editor
{
    abstract class WActor : ITickableObject
    {
        public WTransform Transform { get; protected set; }

        public WActor()
        {
            Transform = new WTransform();
        }

        public abstract void Tick(float deltaTime);
    }
}
