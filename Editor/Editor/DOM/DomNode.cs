using System;
using System.Collections;
using System.Collections.Generic;

namespace WindEditor
{
    public abstract class WDOMNode : IEnumerable<WDOMNode>
    {
        public WDOMNode Parent { get; private set; }
        public WTransform Transform { get; private set; }

        protected List<WDOMNode> m_children;

        public WDOMNode()
        {
            m_children = new List<WDOMNode>();
            Transform = new WTransform();
        }

        public virtual void Tick(float deltaTime)
        {
            foreach (var child in m_children)
                child.Tick(deltaTime);
        }

        public virtual void Render(WSceneView view)
        {
            foreach (var child in m_children)
                child.Render(view);
        }

        public virtual List<T> GetChildrenOfType<T>() where T : WDOMNode
        {
            List<T> result = new List<T>();
            if (this.GetType() == typeof(T))
                result.Add((T)this);

            foreach (var child in m_children)
                result.AddRange(child.GetChildrenOfType<T>());

            return result;
        }

        public virtual AABox GetBoundingBox()
        {
            return new AABox();
        }

        public virtual void SetParent(WDOMNode parent)
        {
            if(parent != null)
            {
                parent.m_children.Add(this);
                Parent = parent;
            }
        }

        public virtual void RemoveChild(WDOMNode item)
        {
            m_children.Remove(item);
            item.Parent = null;
        }

        #region IEnumerable Interface
        public IEnumerator<WDOMNode> GetEnumerator()
        {
            return m_children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
