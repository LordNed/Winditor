using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace WindEditor.a
{
    public abstract class WDOMRenderNode : WDOMNode, IRenderable
    {
        public WTransform GlobalTransform { get; protected set; }

        public WDOMRenderNode(WWorld world, string name) : base(world)
        {
            Name = name;
        }

        #region IRenderable interface
        public virtual void AddToRenderer(WSceneView view) { }
        public virtual void Draw(WSceneView view) { }

        public virtual Vector3 GetPosition() { return GlobalTransform.Position; }
        public virtual float GetBoundingRadius() { return 1.0f; }
        #endregion
    }
}
