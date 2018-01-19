using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace WindEditor
{
	public class Selection : INotifyPropertyChanged
	{
		// WPF
		public event PropertyChangedEventHandler PropertyChanged;

		// Shit that's actually type safe
		public Action OnSelectionChanged;

		public List<WDOMNode> SelectedObjects { get; protected set; }
		public bool SingleObjectSelected { get { return SelectedObjects.Count == 1; } }

		private readonly WWorld m_world;
		private readonly WActorEditor m_actorEditor;

		public Selection(WWorld world, WActorEditor actorEditor)
		{
			SelectedObjects = new List<WDOMNode>();
			m_world = world;
			m_actorEditor = actorEditor;
		}

		public void AddToSelection(WDOMNode Node)
		{
			AddToSelection(new[] { Node });
		}

		public void AddToSelection(IEnumerable<WDOMNode> Nodes)
		{
			WSelectionChangedAction undoAction = new WSelectionChangedAction(this, null, Enumerable.ToArray(Nodes));
			m_world.UndoStack.Push(undoAction);

			BroadcastPropertyChangedNotifications();
		}

		public void RemoveFromSelection(WDOMNode Node)
		{
			RemoveFromSelection(new[] { Node });
		}

		public void RemoveFromSelection(IEnumerable<WDOMNode> Nodes)
		{
			WSelectionChangedAction undoAction = new WSelectionChangedAction(this, Enumerable.ToArray(Nodes), null);
			m_world.UndoStack.Push(undoAction);

			BroadcastPropertyChangedNotifications();
		}

		public void ClearSelection()
		{
			WSelectionChangedAction undoAction = new WSelectionChangedAction(this, Enumerable.ToArray(SelectedObjects), null);
			m_world.UndoStack.Push(undoAction);

			BroadcastPropertyChangedNotifications();
		}

		private void BroadcastPropertyChangedNotifications()
		{
			if (PropertyChanged != null)
			{
				PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SelectedObjects"));
				PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SingleObjectSelected"));
			}

			if (OnSelectionChanged != null)
				OnSelectionChanged.Invoke();
		}
	}
}
