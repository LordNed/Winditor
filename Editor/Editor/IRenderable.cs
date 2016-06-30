using OpenTK;

namespace WindEditor
{
    public interface IRenderable
    {
        void AddToRenderer(WSceneView view);
        void Draw(WSceneView view);
    }
}
