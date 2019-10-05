using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace WindEditor.a
{
    [Flags]
    public enum NodeFlags
    {
        None = 0,
        Selected = 1,
        Expanded = 2,
        Visible = 4,
        Rendered = 8,
        Disposed = 16
    }

    public abstract class WDOMNode : IEnumerable<WDOMNode>, IEquatable<WDOMNode>, INotifyPropertyChanged, IDisposable
    {
        protected ObservableCollection<WDOMNode> m_Children;
        protected bool m_IsDestroyed;

        public WDOMNode Parent { get; private set; }
        public ObservableCollection<WDOMNode> Children { get { return m_Children; } }
        public WWorld World { get; protected set; }
        public string Name { get; protected set; }

        #region Flags
        public NodeFlags Flags { get; set; }

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

                    foreach (var node in Children)
                    {
                        node.IsRendered = value;
                    }

                    OnPropertyChanged("IsRendered");
                }
            }
        }

        public bool IsDisposed
        {
            get { return Flags.HasFlag(NodeFlags.Rendered); }
            set
            {
                if (value != Flags.HasFlag(NodeFlags.Rendered))
                {
                    if (value)
                        Flags |= NodeFlags.Disposed;
                    else
                        Flags &= ~NodeFlags.Disposed;

                    OnPropertyChanged("IsRendered");
                }
            }
        }
        #endregion

        public WDOMNode(WWorld world)
        {
            World = world;
            m_Children = new ObservableCollection<WDOMNode>();

            IsVisible = true;
            m_IsDestroyed = false;
        }

        public virtual void Tick(float deltaTime)
        {
            foreach (var child in m_Children)
                child.Tick(deltaTime);
        }

        public virtual void Destroy()
        {
            m_IsDestroyed = true;
            Parent.RemoveChild(this);
        }

        #region Context Menu Operations
        public ICommand OnRequestCopy { get { return new RelayCommand(x => Copy()); } }
        public ICommand OnRequestCut { get { return new RelayCommand(x => Cut()); } }
        public ICommand OnRequestPaste { get { return new RelayCommand(x => Paste()); } }
        public ICommand OnRequestHideThis { get { return new RelayCommand(x => HideThis()); } }
        public ICommand OnRequestShowOnlyThis { get { return new RelayCommand(x => ShowOnlyThis()); } }
        public ICommand OnRequestShowAll { get { return new RelayCommand(x => ShowAll()); } }

        public virtual ContextMenu GetContextMenu()
        {
            ContextMenu context = new ContextMenu();
            MenuItem copy = new MenuItem() { Header = "Copy", Command = OnRequestCopy };
            MenuItem cut = new MenuItem() { Header = "Cut", Command = OnRequestCut };
            MenuItem paste = new MenuItem() { Header = "Paste", Command = OnRequestPaste };
            MenuItem hide = new MenuItem() { Header = "Hide This", Command = OnRequestHideThis };
            MenuItem showthis = new MenuItem() { Header = "Show Only This", Command = OnRequestShowOnlyThis };
            MenuItem showall = new MenuItem() { Header = "Show All", Command = OnRequestShowAll };

            context.Items.Add(copy);
            context.Items.Add(cut);
            context.Items.Add(paste);
            context.Items.Add(new Separator());
            context.Items.Add(hide);
            context.Items.Add(new Separator());
            context.Items.Add(showthis);
            context.Items.Add(showall);

            return context;
        }

        public virtual void Copy() { }
        public virtual void Cut() { }
        public virtual void Paste() { }
        public virtual void ShowOnlyThis() { }
        public virtual void ShowAll() { }
        public virtual void HideThis() { }
        #endregion

        #region Parent/Child Operations
        public virtual List<T> GetChildrenOfType<T>() where T : class
        {
            List<T> result = new List<T>();
            if (this is T testCast)
                result.Add(testCast);

            foreach (var child in m_Children)
                result.AddRange(child.GetChildrenOfType<T>());

            return result;
        }

        public virtual List<WDOMNode> GetChildrenOfType(Type t)
        {
            List<WDOMNode> result = new List<WDOMNode>();
            if (GetType() == t)
                result.Add(this);

            foreach (var child in m_Children)
                result.AddRange(child.GetChildrenOfType(t));

            return result;
        }

        public virtual void RemoveChild(WDOMNode item)
        {
            m_Children.Remove(item);
            item.Parent = null;
        }

        public virtual void SetParent(WDOMNode newParent)
        {
            if (Parent != null)
                Parent.RemoveChild(this);

            if (newParent != null)
            {
                newParent.m_Children.Add(this);
                Parent = newParent;
            }
        }
        #endregion

        #region Selected Event
        public delegate void SelectedChangedEventHandler(object sender, SelectEventArgs e);
        public event SelectedChangedEventHandler SelectedChanged;

        protected void OnSelectedChanged(SelectEventArgs e)
        {
            SelectedChanged?.Invoke(this, e);
        }
        #endregion

        #region IEnumerable Interface
        public IEnumerator<WDOMNode> GetEnumerator()
        {
            return m_Children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        #region INotifyPropertyChanged Interface
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region IDisposable Interface
        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                IsDisposed = true;
            }
        }

        ~WDOMNode()
        {
           // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
           Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region IEquatable Interface
        public bool Equals(WDOMNode other)
        {
            bool AIsValid = !ReferenceEquals(this, null) ? !m_IsDestroyed : false;
            bool BIsValid = !ReferenceEquals(other, null) ? !other.m_IsDestroyed : false;

            if (AIsValid && BIsValid)
            {
                return ReferenceEquals(this, other);
            }

            return AIsValid == BIsValid;
        }
        #endregion
    }

    public class SelectEventArgs : EventArgs
    {
        public WDOMNode Selection { get; set; }
        public bool WasSelected { get; set; }

        public SelectEventArgs(WDOMNode selected, bool wasSelected)
        {
            Selection = selected;
            WasSelected = wasSelected;
        }
    }
}
