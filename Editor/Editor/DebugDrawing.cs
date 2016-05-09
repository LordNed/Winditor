using System;
using OpenTK;

namespace WindEditor
{
    public partial class WWorld
    {
        public void DebugDrawLine(Vector3 start, Vector3 end, WLinearColor color, float thickness, float lifetime)
        {
            m_persistentLines.DrawLine(start, end, color, thickness, lifetime);
        }

        public void DebugDrawBox(Vector3 min, Vector3 max, WLinearColor color, float thickness, float lifetime)
        {
            m_persistentLines.DrawBox(min, max, color, thickness, lifetime);
        }
    }
}
