using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace WindEditor
{
    public abstract class WDOMNode : IEnumerable<WDOMNode>, INotifyPropertyChanged
    {
        public WDOMNode Parent { get; private set; }
        public WTransform Transform { get; private set; }
        public List<WDOMNode> Children { get { return m_children; } }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public bool IsVisible
        {
            get { return m_isVisible; }
            set
            {
                if (value != m_isVisible)
                {
                    m_isVisible = value;
                    OnPropertyChanged("IsVisible");
                }
            }
        }

        protected List<WDOMNode> m_children;
        protected WWorld m_world;

        private bool m_isVisible;

        public WDOMNode(WWorld world)
        {
            m_world = world;

            m_children = new List<WDOMNode>();
            Transform = new WTransform();
        }

        public virtual void Tick(float deltaTime)
        {
            foreach (var child in m_children)
                child.Tick(deltaTime);
        }

        public virtual void SetTimeOfDay(float timeOfDay)
        {
            foreach (var child in m_children)
                child.SetTimeOfDay(timeOfDay);
        }

        //public virtual void Render(WSceneView view)
        //{
        //    foreach (var child in m_children)
        //        child.Render(view);
        //}

        public virtual List<T> GetChildrenOfType<T>() where T : class
        {
            List<T> result = new List<T>();
            T testCast = this as T;
            if(testCast != null)
                result.Add(testCast);

            foreach (var child in m_children)
                result.AddRange(child.GetChildrenOfType<T>());

            return result;
        }

        public virtual FAABox GetBoundingBox()
        {
            return new FAABox();
        }

        public virtual void SetParent(WDOMNode newParent)
        {
            if (Parent != null)
                Parent.RemoveChild(this);

            if(newParent != null)
            {
                newParent.m_children.Add(this);
                Parent = newParent;
            }
        }

        public virtual void RemoveChild(WDOMNode item)
        {
            m_children.Remove(item);
            item.Parent = null;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
