using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using WindEditor.Editor.Modes;

namespace WindEditor
{
	public class Selection : INotifyPropertyChanged
	{
		// WPF
		public event PropertyChangedEventHandler PropertyChanged;

		// Shit that's actually type safe
		public Action OnSelectionChanged;

		public WDOMNode PrimarySelectedObject { get { if (SelectedObjects.Count == 1) return SelectedObjects[0]; return null; } }
		public List<WDOMNode> SelectedObjects { get; protected set; }
		public bool SingleObjectSelected { get { return SelectedObjects.Count == 1; } }

		private readonly IEditorMode m_mode;
		//private readonly WActorEditor m_actorEditor;

		public Selection(IEditorMode mode)
		{
			SelectedObjects = new List<WDOMNode>();
            m_mode = mode;
		}

		public void AddToSelection(WDOMNode Node)
		{
			AddToSelection(new[] { Node });
		}

		public void AddToSelection(IEnumerable<WDOMNode> Nodes)
		{
			WSelectionChangedAction undoAction = new WSelectionChangedAction(this, null, Enumerable.ToArray(Nodes));
            m_mode.BroadcastUndoEventGenerated(undoAction);

            BroadcastPropertyChangedNotifications();
		}

		public void RemoveFromSelection(WDOMNode Node)
		{
			RemoveFromSelection(new[] { Node });
		}

		public void RemoveFromSelection(IEnumerable<WDOMNode> Nodes)
		{
			WSelectionChangedAction undoAction = new WSelectionChangedAction(this, Enumerable.ToArray(Nodes), null);
            m_mode.BroadcastUndoEventGenerated(undoAction);

            BroadcastPropertyChangedNotifications();
		}

		public void ClearSelection()
		{
			WSelectionChangedAction undoAction = new WSelectionChangedAction(this, Enumerable.ToArray(SelectedObjects), null);
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
