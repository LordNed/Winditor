using OpenTK;
using System.Collections.Generic;

namespace Editor
{
    struct WBatchedLine
    {
        public Vector3 Start;
        public Vector3 End;
        public WLinearColor Color;
        public float Thickness;
        public float RemainingLifetime;

        public WBatchedLine(Vector3 start, Vector3 end, WLinearColor color, float thickness, float lifetime)
        {
            Start = start;
            End = end;
            Color = color;
            Thickness = thickness;
            RemainingLifetime = lifetime;
        }
    }

    class WLineBatcher : PrimitiveComponent
    {
        private List<WBatchedLine> m_batchedLines;

        public WLineBatcher()
        {
            m_batchedLines = new List<WBatchedLine>();
        }

        public void DrawLine(Vector3 start, Vector3 end, WLinearColor color, float thickness, float lifetime)
        {
            m_batchedLines.Add(new WBatchedLine(start, end, color, thickness, lifetime));

            MarkRenderStateAsDirty();
        }

        public void Tick(float deltaTime)
        {
            bool dirty = false;
            for(int lineIndex = 0; lineIndex < m_batchedLines.Count; lineIndex++)
            {
                WBatchedLine line = m_batchedLines[lineIndex];
                if(line.RemainingLifetime > 0)
                {
                    line.RemainingLifetime -= deltaTime;
                    if(line.RemainingLifetime <= 0f)
                    {
                        // Remove the line from the array and deincrement to avoid skipping a line.
                        m_batchedLines.RemoveAt(lineIndex--);
                        dirty = true;
                    }
                }
            }

            if (dirty)
                MarkRenderStateAsDirty();
        }

        public void Flush()
        {
            m_batchedLines.Clear();
        }
    }
}
