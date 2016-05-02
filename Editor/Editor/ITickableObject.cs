namespace Editor
{
    public interface ITickableObject
    {
        void Tick(float deltaTime);
        void SetWorld(WWorld world);
        WWorld GetWorld();
    }
}
