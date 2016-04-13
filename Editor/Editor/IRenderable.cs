using OpenTK;

namespace Editor
{
    public interface IRenderable
    {
        void Render(Matrix4 viewMatrix, Matrix4 projMatrix);
        void ReleaseResources();
    }
}
