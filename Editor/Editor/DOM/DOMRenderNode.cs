using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEditor.a
{
    public class DOMRenderNode : WDOMNode
    {
        public override string Name { get; set; }
        public List<Tuple<IRenderable, WTransform>> Renderables { get; protected set; }
        public FAABox Bounds { get; protected set; }

        public DOMRenderNode(WWorld world, string name) : base(world)
        {
            Name = name;
            Renderables = new List<Tuple<IRenderable, WTransform>>();
        }

        #region Overrides
        public override void Tick(float deltaTime)
        {
            foreach (Tuple<IRenderable, WTransform> t in Renderables)
            {
                t.Item1.
            }

            base.Tick(deltaTime);
        }

        public override string ToString()
        {
            return Name;
        }
        #endregion

        #region IRenderable interface
        public void AddToRenderer(WSceneView view)
        {
            foreach (Tuple<IRenderable, WTransform> t in Renderables)
            {
                t.Item1.AddToRenderer(view);
            }
        }

        public void Draw() { }
        #endregion
    }
}
