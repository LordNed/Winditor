using OpenTK;

namespace WindEditor
{
    public interface IRenderable
    {
        void Render(Matrix4 viewMatrix, Matrix4 projMatrix);
        void ReleaseResources();
    }
}
