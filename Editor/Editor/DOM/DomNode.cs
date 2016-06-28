using System.Collections;
using System.Collections.Generic;

namespace WindEditor
{
    public abstract class WDOMNode : IEnumerable<WDOMNode>
    {
        public List<WDOMNode> Children { get; private set; }

        public WDOMNode()
        {
            Children = new List<WDOMNode>();
        }

        public virtual void Tick(float deltaTime)
        {
            foreach (var child in Children)
                child.Tick(deltaTime);
        }

        public virtual void Render()
        {
            foreach (var child in Children)
                child.Render();
        }

        #region IEnumerable Interface
        public IEnumerator<WDOMNode> GetEnumerator()
        {
            return Children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
