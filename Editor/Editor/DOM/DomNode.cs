using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Xceed.Wpf.Toolkit.PropertyGrid;
using System.Collections.ObjectModel;
using WindEditor.ViewModel;

namespace WindEditor
{
    [Flags]
    public enum NodeFlags
    {
        None = 0,
        Selected = 1,
        Expanded = 2,
        Visible = 4,
        Rendered = 8,
    }

    public class SelectEventArgs : EventArgs
    {
        private WDOMNode m_selection;
        private bool m_wasSelected;

        public WDOMNode Selection
        {
            get { return m_selection; }
            set { m_selection = value; }
        }

        public bool WasSelected
        {
            get { return m_wasSelected; }
            set { m_wasSelected = value; }
        }

        public SelectEventArgs(WDOMNode selected, bool wasSelected)
        {
            Selection = selected;
            WasSelected = wasSelected;
        }
    }

    public class RenderedEventArgs : EventArgs
    {
        private bool m_Rendered;
        public bool IsRendered
        {
            get { return m_Rendered; }
            set { m_Rendered = value; }
        }

        public RenderedEventArgs(bool isRendered)
        {
            IsRendered = isRendered;
        }
    }

    public abstract class WDOMNode : IEnumerable<WDOMNode>, INotifyPropertyChanged
    {
		/* This is a list of the properties we want to show up in the UI */
		public List<PropertyDefinition> VisibleProperties { get; protected set; }

		public WDOMNode Parent { get; private set; }
        [WProperty("Transform", "Transform", true)]
        public WTransform Transform { get; private set; }
        public ObservableCollection<WDOMNode> Children { get { return m_children; } }
        public NodeFlags Flags { get; set; }

        public delegate void SelectedChangedEventHandler(object sender, SelectEventArgs e);
        public delegate void RenderedChangedEventHandler(object sender, RenderedEventArgs e);

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public event SelectedChangedEventHandler SelectedChanged;
        //public event RenderedChangedEventHandler RenderedChanged;

        public bool IsSelected
        {
            get { return Flags.HasFlag(NodeFlags.Selected); }
            set
            {
                if (value != Flags.HasFlag(NodeFlags.Selected))
                {
                    if (value)
                        Flags |= NodeFlags.Selected;
                    else
                        Flags &= ~NodeFlags.Selected;

                    OnSelectedChanged(new SelectEventArgs(this, value));
                    OnPropertyChanged("IsSelected");
                }
            }
        }

        public bool IsExpanded
        {
            get { return Flags.HasFlag(NodeFlags.Expanded); }
            set
            {
                if (value != Flags.HasFlag(NodeFlags.Expanded))
                {
                    if (value)
                        Flags |= NodeFlags.Expanded;
                    else
                        Flags &= ~NodeFlags.Expanded;

                    OnPropertyChanged("IsExpanded");
                }
            }
        }

        public bool IsVisible
        {
            get { return Flags.HasFlag(NodeFlags.Visible); }
            set
            {
                if (value != Flags.HasFlag(NodeFlags.Visible))
                {
                    if (value)
                        Flags |= NodeFlags.Visible;
                    else
                        Flags &= ~NodeFlags.Visible;

                    OnPropertyChanged("IsVisible");
                }
            }
        }

        public bool IsRendered
        {
            get { return Flags.HasFlag(NodeFlags.Rendered); }
            set
            {
                if (value != Flags.HasFlag(NodeFlags.Rendered))
                {
                    if (value)
                        Flags |= NodeFlags.Rendered;
                    else
                        Flags &= ~NodeFlags.Rendered;

                    OnPropertyChanged("IsRendered");
                }
            }
        }

		public virtual string Name { get { return ToString(); } set { } }

        public WWorld World
        {
            get { return m_world; }
        }

		public WScene Scene
		{
			get
			{
				WDOMNode curr_node = this;
				while (curr_node.Parent != null)
				{
					curr_node = curr_node.Parent;
				}
				if (curr_node is WScene)
				{
					return curr_node as WScene;
				}
				else
				{
					return null;
				}
			}
		}

        protected ObservableCollection<WDOMNode> m_children;
        protected WWorld m_world;

        public WDOMNode(WWorld world)
        {
            m_world = world;

            m_children = new ObservableCollection<WDOMNode>();
            Transform = new WTransform();
			VisibleProperties = new List<PropertyDefinition>();

            IsVisible = true;
            m_IsDestroyed = false;
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

        public virtual bool ShouldBeRendered()
        {
            if (!IsRendered)
                return false;

            WDOMNode parent = Parent;
            while (parent != null)
            {
                if (!parent.IsRendered)
                    return false;
                parent = parent.Parent;
            }

            return true;
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

        public virtual List<WDOMNode> GetChildrenOfType(Type t)
        {
            List<WDOMNode> result = new List<WDOMNode>();
            if (GetType() == t)
                result.Add(this);

            foreach (var child in m_children)
                result.AddRange(child.GetChildrenOfType(t));

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

        public virtual void Destroy()
        {
            m_IsDestroyed = true;
            Parent.RemoveChild(this);
        }

        public virtual void Undestroy(WDOMNode parent)
        {
            m_IsDestroyed = false;
            SetParent(parent);
        }

        public static bool operator ==(WDOMNode node1, WDOMNode node2)
        {
            bool AIsValid = !ReferenceEquals(node1, null) ? !node1.m_IsDestroyed : false;
            bool BIsValid = !ReferenceEquals(node2, null) ? !node2.m_IsDestroyed : false;

            if (AIsValid && BIsValid)
            {
                return ReferenceEquals(node1, node2);
            }

            return AIsValid == BIsValid;
        }

        public static bool operator !=(WDOMNode node1, WDOMNode node2)
        {
            return !(node1 == node2);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void OnSelectedChanged(SelectEventArgs e)
        {
            if (SelectedChanged != null)
                SelectedChanged(this, e);
        }

        private bool m_IsDestroyed;

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
