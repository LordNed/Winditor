using System.Collections.Generic;

namespace WindEditor
{
    class WSelectionChangedAction : WUndoCommand
    {
		private readonly WDOMNode[] m_removedEntity;
		private readonly WDOMNode[] m_addedEntity;
		private readonly Selection m_selection;

		public WSelectionChangedAction(Selection parentSelection, WDOMNode[] removedActors, WDOMNode[] addedActors)
		{
			this.m_selection = parentSelection;
			m_removedEntity = removedActors != null ? removedActors : new WDOMNode[0];
			m_addedEntity = addedActors != null ? addedActors : new WDOMNode[0];
		}

        public override void Redo()
        {
            base.Redo();

			foreach(var entity in m_removedEntity)
			{
				m_selection.SelectedObjects.Remove(entity);
				entity.IsSelected = false;
			}

			foreach(var entity in m_addedEntity)
			{
				m_selection.SelectedObjects.Add(entity);
				entity.IsSelected = true;
			}
		}

        public override void Undo()
        {
            base.Undo();

			foreach (var entity in m_addedEntity)
			{
				m_selection.SelectedObjects.Remove(entity);
				entity.IsSelected = false;
			}

			foreach (var entity in m_removedEntity)
			{
				m_selection.SelectedObjects.Add(entity);
				entity.IsSelected = true;
			}
        }
    }
}
