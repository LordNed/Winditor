using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using WindEditor.Editor.Modes;

namespace WindEditor
{
	public class Selection<T> : INotifyPropertyChanged
	{
		// WPF
		public event PropertyChangedEventHandler PropertyChanged;

		// Shit that's actually type safe
		public Action OnSelectionChanged;

		public T PrimarySelectedObject { get { if (SelectedObjects.Count >= 1) { return SelectedObjects[0]; } return default(T); } }
		public List<T> SelectedObjects { get; protected set; }
		public bool SingleObjectSelected { get { return SelectedObjects.Count == 1; } }

		private readonly IEditorMode m_mode;

		public Selection(IEditorMode mode)
		{
			SelectedObjects = new List<T>();
            m_mode = mode;
		}

		public void AddToSelection(T Node)
		{
			AddToSelection(new[] { Node });
		}

		public void AddToSelection(IEnumerable<T> Nodes)
		{
			WSelectionChangedAction<T> undoAction = new WSelectionChangedAction<T>(this, null, Enumerable.ToArray(Nodes));
            m_mode.BroadcastUndoEventGenerated(undoAction);

            BroadcastPropertyChangedNotifications();
		}

		public void RemoveFromSelection(T Node)
		{
			RemoveFromSelection(new[] { Node });
		}

		public void RemoveFromSelection(IEnumerable<T> Nodes)
		{
			WSelectionChangedAction<T> undoAction = new WSelectionChangedAction<T>(this, Enumerable.ToArray(Nodes), null);
            m_mode.BroadcastUndoEventGenerated(undoAction);

            BroadcastPropertyChangedNotifications();
		}

		public void ClearSelection()
		{
			WSelectionChangedAction<T> undoAction = new WSelectionChangedAction<T>(this, Enumerable.ToArray(SelectedObjects), null);
            m_mode.BroadcastUndoEventGenerated(undoAction);

            BroadcastPropertyChangedNotifications();
		}

		private void BroadcastPropertyChangedNotifications()
		{
			if (PropertyChanged != null)
			{
				PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SelectedObjects"));
				PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SingleObjectSelected"));
				PropertyChanged.Invoke(this, new PropertyChangedEventArgs("PrimarySelectedObject"));
			}

			if (OnSelectionChanged != null)
				OnSelectionChanged.Invoke();
		}
	}
}
