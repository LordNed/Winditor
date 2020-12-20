using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace WindEditor
{
    // From https://siderite.dev/blog/bindinglist-vs-observablecollection.html/#at3459445455
    internal static class SecurityUtils
    {
        private static volatile ReflectionPermission _memberAccessPermission;
        private static volatile ReflectionPermission _restrictedMemberAccessPermission;

        private static ReflectionPermission memberAccessPermission =>
            _memberAccessPermission ??
            (_memberAccessPermission = new ReflectionPermission(ReflectionPermissionFlag.MemberAccess));

        private static ReflectionPermission restrictedMemberAccessPermission =>
            _restrictedMemberAccessPermission ??
            (_restrictedMemberAccessPermission =
                new ReflectionPermission(ReflectionPermissionFlag.RestrictedMemberAccess));

        private static void demandReflectionAccess(Type type)
        {
            try
            {
                memberAccessPermission.Demand();
            }
            catch (SecurityException)
            {
                demandGrantSet(type.Assembly);
            }
        }

        [SecuritySafeCritical]
        private static void demandGrantSet(Assembly assembly)
        {
            var permissionSet = assembly.PermissionSet;
            var accessPermission = restrictedMemberAccessPermission;
            permissionSet.AddPermission(accessPermission);
            permissionSet.Demand();
        }

        private static bool hasReflectionPermission(Type type)
        {
            try
            {
                demandReflectionAccess(type);
                return true;
            }
            catch (SecurityException)
            {
            }
            return false;
        }

        internal static object SecureCreateInstance(Type type)
        {
            return SecureCreateInstance(type, null, false);
        }

        internal static object SecureCreateInstance(Type type, object[] args, bool allowNonPublic)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            var bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance;
            if (!type.IsVisible)
                demandReflectionAccess(type);
            else if (allowNonPublic && !hasReflectionPermission(type))
                allowNonPublic = false;
            if (allowNonPublic)
                bindingAttr |= BindingFlags.NonPublic;
            return Activator.CreateInstance(type, bindingAttr, null, args, null);
        }

        internal static object SecureCreateInstance(Type type, object[] args)
        {
            return SecureCreateInstance(type, args, false);
        }

        internal static object SecureConstructorInvoke(Type type, Type[] argTypes, object[] args, bool allowNonPublic)
        {
            return SecureConstructorInvoke(type, argTypes, args, allowNonPublic, BindingFlags.Default);
        }

        internal static object SecureConstructorInvoke(Type type, Type[] argTypes, object[] args, bool allowNonPublic,
            BindingFlags extraFlags)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            if (!type.IsVisible)
                demandReflectionAccess(type);
            else if (allowNonPublic && !hasReflectionPermission(type))
                allowNonPublic = false;
            var bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | extraFlags;
            if (!allowNonPublic)
                bindingAttr &= ~BindingFlags.NonPublic;
            var constructor = type.GetConstructor(bindingAttr, null, argTypes, null);
            return constructor?.Invoke(args);
        }

        private static bool genericArgumentsAreVisible(MethodInfo method)
        {
            return !method.IsGenericMethod || method.GetGenericArguments().All(type => type.IsVisible);
        }

        internal static object FieldInfoGetValue(FieldInfo field, object target)
        {
            var declaringType = field.DeclaringType;
            if (declaringType == null)
            {
                if (!field.IsPublic)
                    demandGrantSet(field.Module.Assembly);
            }
            else if (!declaringType.IsVisible || !field.IsPublic)
                demandReflectionAccess(declaringType);
            return field.GetValue(target);
        }

        internal static object MethodInfoInvoke(MethodInfo method, object target, object[] args)
        {
            var declaringType = method.DeclaringType;
            if (declaringType == null)
            {
                if (!method.IsPublic || !genericArgumentsAreVisible(method))
                    demandGrantSet(method.Module.Assembly);
            }
            else if (!declaringType.IsVisible || !method.IsPublic || !genericArgumentsAreVisible(method))
                demandReflectionAccess(declaringType);
            return method.Invoke(target, args);
        }

        internal static object ConstructorInfoInvoke(ConstructorInfo ctor, object[] args)
        {
            var declaringType = ctor.DeclaringType;
            if (declaringType != null && (!declaringType.IsVisible || !ctor.IsPublic))
                demandReflectionAccess(declaringType);
            return ctor.Invoke(args);
        }

        internal static object ArrayCreateInstance(Type type, int length)
        {
            if (!type.IsVisible)
                demandReflectionAccess(type);
            return Array.CreateInstance(type, length);
        }
    }

    public class AdvancedCollection<T> : IList<T>, IList, IReadOnlyList<T>
    {
        private readonly List<T> _innerList;
        private readonly Dictionary<T, List<int>> _reverseIndex;
        private readonly object _syncRoot = new object();

        public AdvancedCollection()
        {
            _innerList = new List<T>();
            _reverseIndex = new Dictionary<T, List<int>>();
        }

        public AdvancedCollection(IList<T> list) : this()
        {
            _innerList.Clear();
            _innerList.AddRange(list);
            refreshIndexes();
        }

        private void refreshIndexes()
        {
            lock (_syncRoot)
            {
                _reverseIndex.Clear();
                for (var i = 0; i < _innerList.Count; i++)
                {
                    var item = _innerList[i];
                    List<int> indexes;
                    if (!_reverseIndex.TryGetValue(item, out indexes))
                    {
                        indexes = new List<int>();
                        _reverseIndex[item] = indexes;
                    }
                    indexes.Add(i);
                }
            }
        }

        public int IndexOf(T item)
        {
            List<int> indexes;
            if (!_reverseIndex.TryGetValue(item, out indexes)) return -1;
            return indexes.Count == 0 ? -1 : indexes[0];
        }

        public void Insert(int index, T item)
        {
            lock (_syncRoot)
            {
                var indexesList = Enumerable.Range(index, _innerList.Count - index)
                    .Select(i => _reverseIndex[_innerList[i]])
                    .Distinct();
                foreach (var indexes in indexesList)
                {
                    for (var j = 0; j < indexes.Count; j++)
                    {
                        if (indexes[j] >= index)
                        {
                            indexes[j]++;
                        }
                    }
                }
                _innerList.Insert(index, item);
            }
        }

        public void RemoveAt(int index)
        {
            lock (_syncRoot)
            {
                var indexesList = Enumerable.Range(index, _innerList.Count - index)
                    .Select(i => _reverseIndex[_innerList[i]])
                    .Distinct();
                foreach (var indexes in indexesList)
                {
                    for (var j = 0; j < indexes.Count; j++)
                    {
                        var i = indexes[j];
                        if (i == index)
                        {
                            indexes.RemoveAt(j);
                            j--;
                            continue;
                        }
                        if (indexes[j] > index)
                        {
                            indexes[j]--;
                        }
                    }
                }
                _innerList.RemoveAt(index);
            }
        }

        public T this[int index]
        {
            get { return _innerList[index]; }
            set
            {
                lock (_syncRoot)
                {
                    var indexes = _reverseIndex[_innerList[index]];
                    indexes.Remove(index);
                    if (!_reverseIndex.TryGetValue(value, out indexes))
                    {
                        indexes = new List<int>();
                        _reverseIndex[value] = indexes;
                    }
                    indexes.Add(index);
                    indexes.Sort();
                }
            }
        }


        int IList.Add(object value)
        {
            return add((T)value);
        }

        bool IList.Contains(object value)
        {
            return ((IList<T>)this).Contains((T)value);
        }

        void IList.Clear()
        {
            lock (_syncRoot)
            {
                _innerList.Clear();
                _reverseIndex.Clear();
            }
        }

        int IList.IndexOf(object value)
        {
            return ((IList<T>)this).IndexOf((T)value);
        }

        void IList.Insert(int index, object value)
        {
            ((IList<T>)this).Insert(index, (T)value);
        }

        void IList.Remove(object value)
        {
            var index = ((IList)this).IndexOf(value);
            if (index < 0) return;
            ((IList)this).RemoveAt(index);
        }

        void IList.RemoveAt(int index)
        {
            ((IList<T>)this).RemoveAt(index);
        }

        object IList.this[int index]
        {
            get { return ((IList<T>)this)[index]; }
            set
            {
                var item = (T)value;
                ((IList<T>)this)[index] = item;
            }
        }

        bool IList.IsReadOnly => false;

        bool IList.IsFixedSize => false;

        void ICollection.CopyTo(Array array, int index)
        {
            ((IList)_innerList).CopyTo(array, index);
        }

        public int Count => _innerList.Count;

        object ICollection.SyncRoot => _syncRoot;

        bool ICollection.IsSynchronized => false;

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _innerList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
        }

        public void Add(T item)
        {
            add(item);
        }

        public void Clear()
        {
            ((IList)this).Clear();
        }

        bool ICollection<T>.Contains(T item)
        {
            lock (_syncRoot)
            {
                return _reverseIndex.ContainsKey(item);
            }
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            lock (_syncRoot)
            {
                _innerList.CopyTo(array, arrayIndex);
            }
        }

        bool ICollection<T>.Remove(T item)
        {
            var index = ((IList<T>)this).IndexOf(item);
            if (index < 0) return false;
            ((IList<T>)this).RemoveAt(index);
            return true;
        }

        int ICollection<T>.Count => _innerList.Count;

        bool ICollection<T>.IsReadOnly => false;

        int IReadOnlyCollection<T>.Count => _innerList.Count;

        T IReadOnlyList<T>.this[int index] => ((IList<T>)this)[index];

        private int add(T item)
        {
            lock (_syncRoot)
            {
                var index = _innerList.Count;
                _innerList.Add(item);
                List<int> indexes;
                if (!_reverseIndex.TryGetValue(item, out indexes))
                {
                    indexes = new List<int>();
                    _reverseIndex[item] = indexes;
                }
                indexes.Add(index);
                return index;
            }
        }

        protected virtual void ClearItems()
        {
            ((IList<T>)this).Clear();
        }

        protected virtual void InsertItem(int index, T item)
        {
            ((IList<T>)this).Insert(index, item);
        }

        protected virtual void RemoveItem(int index)
        {
            ((IList<T>)this).RemoveAt(index);
        }

        protected virtual void SetItem(int index, T item)
        {
            this[index] = item;
        }
    }

    /// <summary>
    ///     Provides a generic collection that supports data binding.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    [Serializable]
    [HostProtection(SecurityAction.LinkDemand, SharedState = true)]
    public class AdvancedBindingList<T> : AdvancedCollection<T>, IBindingList, ICancelAddNew, IRaiseItemChangedEvents,
        INotifyCollectionChanged
    {
        private int _addNewPos = -1;
        private bool _allowEdit = true;
        private bool _allowNew = true;
        private bool _allowRemove = true;

        [NonSerialized] private PropertyDescriptorCollection _itemTypeProperties;

        [NonSerialized] private int _lastChangeIndex = -1;

        [NonSerialized] private AddingNewEventHandler _onAddingNew;

        [NonSerialized] private PropertyChangedEventHandler _propertyChangedEventHandler;

        private bool _raiseItemChangedEvents;
        private bool _raiseListChangedEvents = true;
        private bool _userSetAllowNew;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.ComponentModel.BindingList`1" /> class using default values.
        /// </summary>
        public AdvancedBindingList()
        {
            initialize();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.ComponentModel.BindingList`1" /> class with the specified
        ///     list.
        /// </summary>
        /// <param name="list">
        ///     An <see cref="T:System.Collections.Generic.IList`1" /> of items to be contained in the
        ///     <see cref="T:System.ComponentModel.BindingList`1" />.
        /// </param>
        public AdvancedBindingList(IList<T> list)
            : base(list)
        {
            initialize();
        }

        private bool itemTypeHasDefaultConstructor
        {
            get
            {
                var type = typeof(T);
                return type.IsPrimitive ||
                        type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance,
                            null, new Type[0], null) != null;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether adding or removing items within the list raises
        ///     <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> events.
        /// </summary>
        /// <returns>
        ///     true if adding or removing items raises <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> events;
        ///     otherwise, false. The default is true.
        /// </returns>
        public bool RaiseListChangedEvents
        {
            get { return _raiseListChangedEvents; }
            set { _raiseListChangedEvents = value; }
        }

        private bool addingNewHandled
        {
            get
            {
                if (_onAddingNew != null)
                    return (uint)_onAddingNew.GetInvocationList().Length > 0U;
                return false;
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether you can add items to the list using the
        ///     <see cref="M:System.ComponentModel.BindingList`1.AddNew" /> method.
        /// </summary>
        /// <returns>
        ///     true if you can add items to the list with the <see cref="M:System.ComponentModel.BindingList`1.AddNew" /> method;
        ///     otherwise, false. The default depends on the underlying type contained in the list.
        /// </returns>
        public bool AllowNew
        {
            get
            {
                if (_userSetAllowNew || _allowNew)
                    return _allowNew;
                return addingNewHandled;
            }
            set
            {
                var num1 = AllowNew ? 1 : 0;
                _userSetAllowNew = true;
                _allowNew = value;
                var num2 = value ? 1 : 0;
                if (num1 == num2)
                    return;
                fireListReset();
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether items in the list can be edited.
        /// </summary>
        /// <returns>
        ///     true if list items can be edited; otherwise, false. The default is true.
        /// </returns>
        public bool AllowEdit
        {
            get { return _allowEdit; }
            set
            {
                if (_allowEdit == value)
                    return;
                _allowEdit = value;
                fireListReset();
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether you can remove items from the collection.
        /// </summary>
        /// <returns>
        ///     true if you can remove items from the list with the
        ///     <see cref="M:System.ComponentModel.BindingList`1.RemoveItem(System.Int32)" /> method otherwise, false. The default
        ///     is true.
        /// </returns>
        public bool AllowRemove
        {
            get { return _allowRemove; }
            set
            {
                if (_allowRemove == value)
                    return;
                _allowRemove = value;
                fireListReset();
            }
        }

        /// <summary>
        ///     Gets a value indicating whether <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> events are
        ///     enabled.
        /// </summary>
        /// <returns>
        ///     true if <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> events are supported; otherwise, false.
        ///     The default is true.
        /// </returns>
        protected virtual bool SupportsChangeNotificationCore => true;

        /// <summary>
        ///     Gets a value indicating whether the list supports searching.
        /// </summary>
        /// <returns>
        ///     true if the list supports searching; otherwise, false. The default is false.
        /// </returns>
        protected virtual bool SupportsSearchingCore => false;

        /// <summary>
        ///     Gets a value indicating whether the list supports sorting.
        /// </summary>
        /// <returns>
        ///     true if the list supports sorting; otherwise, false. The default is false.
        /// </returns>
        protected virtual bool SupportsSortingCore => false;

        /// <summary>
        ///     Gets a value indicating whether the list is sorted.
        /// </summary>
        /// <returns>
        ///     true if the list is sorted; otherwise, false. The default is false.
        /// </returns>
        protected virtual bool IsSortedCore => false;

        /// <summary>
        ///     Gets the property descriptor that is used for sorting the list if sorting is implemented in a derived class;
        ///     otherwise, returns null.
        /// </summary>
        /// <returns>
        ///     The <see cref="T:System.ComponentModel.PropertyDescriptor" /> used for sorting the list.
        /// </returns>
        protected virtual PropertyDescriptor SortPropertyCore => null;

        /// <summary>
        ///     Gets the direction the list is sorted.
        /// </summary>
        /// <returns>
        ///     One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values. The default is
        ///     <see cref="F:System.ComponentModel.ListSortDirection.Ascending" />.
        /// </returns>
        protected virtual ListSortDirection SortDirectionCore => ListSortDirection.Ascending;

        bool IBindingList.AllowNew => AllowNew;

        bool IBindingList.AllowEdit => AllowEdit;

        bool IBindingList.AllowRemove => AllowRemove;

        bool IBindingList.SupportsChangeNotification => SupportsChangeNotificationCore;

        bool IBindingList.SupportsSearching => SupportsSearchingCore;

        bool IBindingList.SupportsSorting => SupportsSortingCore;

        bool IBindingList.IsSorted => IsSortedCore;

        PropertyDescriptor IBindingList.SortProperty => SortPropertyCore;

        ListSortDirection IBindingList.SortDirection => SortDirectionCore;

        /// <summary>
        ///     Occurs when the list or an item in the list changes.
        /// </summary>
        public event ListChangedEventHandler ListChanged;

        object IBindingList.AddNew()
        {
            var obj = AddNewCore();
            _addNewPos = obj != null ? ((IList<T>)this).IndexOf((T)obj) : -1;
            return obj;
        }

        void IBindingList.ApplySort(PropertyDescriptor prop, ListSortDirection direction)
        {
            ApplySortCore(prop, direction);
        }

        void IBindingList.RemoveSort()
        {
            RemoveSortCore();
        }

        int IBindingList.Find(PropertyDescriptor prop, object key)
        {
            return FindCore(prop, key);
        }

        void IBindingList.AddIndex(PropertyDescriptor prop)
        {
        }

        void IBindingList.RemoveIndex(PropertyDescriptor prop)
        {
        }

        /// <summary>
        ///     Discards a pending new item.
        /// </summary>
        /// <param name="itemIndex">The index of the of the new item to be added </param>
        public virtual void CancelNew(int itemIndex)
        {
            if (_addNewPos < 0 || _addNewPos != itemIndex)
                return;
            RemoveItem(_addNewPos);
            _addNewPos = -1;
        }

        /// <summary>
        ///     Commits a pending new item to the collection.
        /// </summary>
        /// <param name="itemIndex">The index of the new item to be added.</param>
        public virtual void EndNew(int itemIndex)
        {
            if (_addNewPos < 0 || _addNewPos != itemIndex)
                return;
            _addNewPos = -1;
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        bool IRaiseItemChangedEvents.RaisesItemChangedEvents => _raiseItemChangedEvents;

        private void fireListReset()
        {
            fireListChanged(ListChangedType.Reset, -1);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        /// <summary>
        ///     Occurs before an item is added to the list.
        /// </summary>
        public event AddingNewEventHandler AddingNew
        {
            add
            {
                var num1 = AllowNew ? 1 : 0;
                _onAddingNew = _onAddingNew + value;
                var num2 = AllowNew ? 1 : 0;
                if (num1 == num2)
                    return;
                fireListReset();
            }
            remove
            {
                var num1 = AllowNew ? 1 : 0;
                // ReSharper disable once DelegateSubtraction
                _onAddingNew = _onAddingNew - value;
                var num2 = AllowNew ? 1 : 0;
                if (num1 == num2)
                    return;
                fireListReset();
            }
        }

        private void initialize()
        {
            _allowNew = itemTypeHasDefaultConstructor;
            if (!typeof(INotifyPropertyChanged).IsAssignableFrom(typeof(T)))
                return;
            _raiseItemChangedEvents = true;
            foreach (var obj in this)
                hookPropertyChanged(obj);
        }

        /// <summary>
        ///     Raises the <see cref="E:System.ComponentModel.BindingList`1.AddingNew" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.ComponentModel.AddingNewEventArgs" /> that contains the event data. </param>
        protected virtual void OnAddingNew(AddingNewEventArgs e)
        {
            _onAddingNew?.Invoke(this, e);
        }

        private object fireAddingNew()
        {
            var e = new AddingNewEventArgs(null);
            OnAddingNew(e);
            return e.NewObject;
        }

        /// <summary>
        ///     Raises the <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.ComponentModel.ListChangedEventArgs" /> that contains the event data. </param>
        protected virtual void OnListChanged(ListChangedEventArgs e)
        {
            ListChanged?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises a <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> event of type
        ///     <see cref="F:System.ComponentModel.ListChangedType.Reset" />.
        /// </summary>
        public void ResetBindings()
        {
            fireListReset();
        }

        /// <summary>
        ///     Raises a <see cref="E:System.ComponentModel.BindingList`1.ListChanged" /> event of type
        ///     <see cref="F:System.ComponentModel.ListChangedType.ItemChanged" /> for the item at the specified position.
        /// </summary>
        /// <param name="position">A zero-based index of the item to be reset.</param>
        public void ResetItem(int position)
        {
            fireListChanged(ListChangedType.ItemChanged, position);
            CollectionChanged?.Invoke(this,
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, this[position], position));
        }

        private void fireListChanged(ListChangedType type, int index)
        {
            if (!_raiseListChangedEvents)
                return;
            OnListChanged(new ListChangedEventArgs(type, index));
        }

        /// <summary>
        ///     Removes all elements from the collection.
        /// </summary>
        protected override void ClearItems()
        {
            EndNew(_addNewPos);
            if (_raiseItemChangedEvents)
            {
                foreach (var obj in this)
                    unhookPropertyChanged(obj);
            }
            base.ClearItems();
            fireListReset();
        }

        /// <summary>
        ///     Inserts the specified item in the list at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index where the item is to be inserted.</param>
        /// <param name="item">The item to insert in the list.</param>
        protected override void InsertItem(int index, T item)
        {
            EndNew(_addNewPos);
            base.InsertItem(index, item);
            if (_raiseItemChangedEvents)
                hookPropertyChanged(item);
            fireListChanged(ListChangedType.ItemAdded, index);
            CollectionChanged?.Invoke(this,
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
        }

        /// <summary>
        ///     Removes the item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove. </param>
        /// <exception cref="T:System.NotSupportedException">
        ///     You are removing a newly added item and
        ///     <see cref="P:System.ComponentModel.IBindingList.AllowRemove" /> is set to false.
        /// </exception>
        protected override void RemoveItem(int index)
        {
            if (!_allowRemove && (_addNewPos < 0 || _addNewPos != index))
                throw new NotSupportedException();
            EndNew(_addNewPos);
            if (_raiseItemChangedEvents)
                unhookPropertyChanged(this[index]);
            var item = this[index];
            base.RemoveItem(index);
            fireListChanged(ListChangedType.ItemDeleted, index);
            CollectionChanged?.Invoke(this,
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
        }

        /// <summary>
        ///     Replaces the item at the specified index with the specified item.
        /// </summary>
        /// <param name="index">The zero-based index of the item to replace.</param>
        /// <param name="item">The new value for the item at the specified index. The value can be null for reference types.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="index" /> is less than zero.-or-
        ///     <paramref name="index" /> is greater than <see cref="P:System.Collections.ObjectModel.Collection`1.Count" />.
        /// </exception>
        protected override void SetItem(int index, T item)
        {
            if (_raiseItemChangedEvents)
                unhookPropertyChanged(this[index]);
            base.SetItem(index, item);
            if (_raiseItemChangedEvents)
                hookPropertyChanged(item);
            fireListChanged(ListChangedType.ItemChanged, index);
            CollectionChanged?.Invoke(this,
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, item, index));
        }

        /// <summary>
        ///     Adds a new item to the collection.
        /// </summary>
        /// <returns>
        ///     The item added to the list.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">
        ///     The <see cref="P:System.Windows.Forms.BindingSource.AllowNew" />
        ///     property is set to false. -or-A public default constructor could not be found for the current item type.
        /// </exception>
        public T AddNew()
        {
            return (T)((IBindingList)this).AddNew();
        }

        /// <summary>
        ///     Adds a new item to the collection with the given arguments.
        /// </summary>
        /// <returns>
        ///     The item added to the list.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">
        ///     The <see cref="P:System.Windows.Forms.BindingSource.AllowNew" />
        ///     property is set to false. -or-A public default constructor could not be found for the current item type.
        /// </exception>
        public T AddNew(object[] args)
        {
            var obj = AddNewCore(args);
            _addNewPos = obj != null ? ((IList<T>)this).IndexOf((T)obj) : -1;
            return (T)obj;
        }

        /// <summary>
        ///     Adds a new item to the end of the collection.
        /// </summary>
        /// <returns>
        ///     The item that was added to the collection.
        /// </returns>
        /// <exception cref="T:System.InvalidCastException">
        ///     The new item is not the same type as the objects contained in the
        ///     <see cref="T:System.ComponentModel.BindingList`1" />.
        /// </exception>
        protected virtual object AddNewCore()
        {
            var obj = fireAddingNew() ?? getNewInstance();
            Add((T)obj);
            return obj;
        }

        /// <summary>
        ///     Adds a new item with the given arguments to the end of the collection.
        /// </summary>
        /// <returns>
        ///     The item that was added to the collection.
        /// </returns>
        /// <exception cref="T:System.InvalidCastException">
        ///     The new item is not the same type as the objects contained in the
        ///     <see cref="T:System.ComponentModel.BindingList`1" />.
        /// </exception>
        protected virtual object AddNewCore(object[] args)
        {
            var obj = fireAddingNew() ?? getNewInstance(args);
            Add((T)obj);
            return obj;
        }

        private static object getNewInstance()
        {
            return SecurityUtils.SecureCreateInstance(typeof(T));
        }

        private static object getNewInstance(object[] args)
        {
            return SecurityUtils.SecureCreateInstance(typeof(T), args);
        }

        /// <summary>
        ///     Sorts the items if overridden in a derived class; otherwise, throws a <see cref="T:System.NotSupportedException" />
        ///     .
        /// </summary>
        /// <param name="prop">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that specifies the property to sort on.</param>
        /// <param name="direction">One of the <see cref="T:System.ComponentModel.ListSortDirection" />  values.</param>
        /// <exception cref="T:System.NotSupportedException">Method is not overridden in a derived class. </exception>
        protected virtual void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        ///     Removes any sort applied with
        ///     <see
        ///         cref="M:System.ComponentModel.BindingList`1.ApplySortCore(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)" />
        ///     if sorting is implemented in a derived class; otherwise, raises <see cref="T:System.NotSupportedException" />.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">Method is not overridden in a derived class. </exception>
        protected virtual void RemoveSortCore()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        ///     Searches for the index of the item that has the specified property descriptor with the specified value, if
        ///     searching is implemented in a derived class; otherwise, a <see cref="T:System.NotSupportedException" />.
        /// </summary>
        /// <returns>
        ///     The zero-based index of the item that matches the property descriptor and contains the specified value.
        /// </returns>
        /// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to search for.</param>
        /// <param name="key">
        ///     The value of
        ///     <paramref>
        ///         <name>property</name>
        ///     </paramref>
        ///     to match.
        /// </param>
        /// <exception cref="T:System.NotSupportedException">
        ///     <see cref="M:System.ComponentModel.BindingList`1.FindCore(System.ComponentModel.PropertyDescriptor,System.Object)" />
        ///     is not overridden in a derived class.
        /// </exception>
        protected virtual int FindCore(PropertyDescriptor prop, object key)
        {
            throw new NotSupportedException();
        }

        private void hookPropertyChanged(T item)
        {
            var notifyPropertyChanged = (object)item as INotifyPropertyChanged;
            if (notifyPropertyChanged == null)
                return;
            if (_propertyChangedEventHandler == null)
                _propertyChangedEventHandler = Child_PropertyChanged;
            notifyPropertyChanged.PropertyChanged += _propertyChangedEventHandler;
        }

        private void unhookPropertyChanged(T item)
        {
            var notifyPropertyChanged = (object)item as INotifyPropertyChanged;
            if (notifyPropertyChanged == null || _propertyChangedEventHandler == null)
                return;
            notifyPropertyChanged.PropertyChanged -= _propertyChangedEventHandler;
        }

        private void Child_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!RaiseListChangedEvents)
                return;
            if (sender != null && e != null)
            {
                if (!string.IsNullOrEmpty(e.PropertyName))
                {
                    T obj;
                    try
                    {
                        obj = (T)sender;
                    }
                    catch (InvalidCastException)
                    {
                        ResetBindings();
                        return;
                    }
                    var newIndex = _lastChangeIndex;
                    if (newIndex < 0 || newIndex >= Count || !this[newIndex].Equals(obj))
                    {
                        newIndex = IndexOf(obj);
                        _lastChangeIndex = newIndex;
                    }
                    if (newIndex == -1)
                    {
                        unhookPropertyChanged(obj);
                        ResetBindings();
                        return;
                    }
                    if (_itemTypeProperties == null)
                        _itemTypeProperties = TypeDescriptor.GetProperties(typeof(T));
                    var propDesc = _itemTypeProperties.Find(e.PropertyName, true);
                    OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, newIndex, propDesc));
                    return;
                }
            }
            ResetBindings();
        }
    }
}
