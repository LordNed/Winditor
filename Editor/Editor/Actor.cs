using System;
using OpenTK;

namespace WindEditor
{
    abstract class WActor : ITickableObject
    {
        public WTransform Transform { get; protected set; }

        private WWorld m_world;

        public WActor()
        {
            Transform = new WTransform();
        }

        public abstract void Tick(float deltaTime);

        public virtual AABox GetAABB()
        {
            return new AABox();
        }

        public void SetWorld(WWorld world)
        {
            m_world = world;
        }

        public WWorld GetWorld()
        {
            return m_world;
        }
    }
}
