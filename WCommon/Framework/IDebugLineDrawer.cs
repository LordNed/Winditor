using OpenTK;

namespace WindEditor
{
    public interface IDebugLineDrawer
    {
        void DrawLine(Vector3 start, Vector3 end, WLinearColor color, float thickness, float lifetime);
        void DrawBox(Vector3 min, Vector3 max, WLinearColor color, float thickness, float lifetime);
        void DrawBox(Vector3 center, Vector3 box, Quaternion rotation, WLinearColor color, float lifetime, float thickness);
        void DrawSphere(Vector3 center, float radius, int segments, WLinearColor color, float lifetime, float thickness);
    }
}
