using System.Collections;
using System.Collections.Generic;

namespace WindEditor
{
    public abstract class WDOMNode : IEnumerable<WDOMNode>
    {
        public List<WDOMNode> Children { get; private set; }
        public WTransform Transform { get; private set; }

        public WDOMNode()
        {
            Children = new List<WDOMNode>();
            Transform = new WTransform();
        }

        public virtual void Tick(float deltaTime)
        {
            foreach (var child in Children)
                child.Tick(deltaTime);
        }

        public virtual void Render(WSceneView view)
        {
            foreach (var child in Children)
                child.Render(view);
        }

        public virtual List<T> GetChildrenOfType<T>() where T : WDOMNode
        {
            List<T> result = new List<T>();
            if (this.GetType() == typeof(T))
                result.Add((T)this);

            foreach (var child in Children)
                result.AddRange(child.GetChildrenOfType<T>());

            return result;
        }

        public virtual AABox GetBoundingBox()
        {
            return new AABox();
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
