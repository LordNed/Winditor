﻿using System;
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

        public void DebugDrawBox(Vector3 center, Vector3 extents, Quaternion rotation, WLinearColor color, float thickness, float lifetime)
        {
            m_persistentLines.DrawBox(center, extents, rotation, color, lifetime, thickness);
        }

        public void DebugDrawQuad(string texture_name, Vector3 position, Vector3 scale, WLinearColor color, float lifetime)
        {
            m_persistentQuads.DrawQuad(texture_name, position, scale, color, lifetime);
        }

        public void DebugDrawBillboard(string texture_name, Vector3 position, Vector3 scale, WLinearColor color, float lifetime)
        {
            m_persistentQuads.DrawBillboard(texture_name, position, scale, color, lifetime);
        }
    }
}
