using System.Collections.Generic;

namespace WindEditor
{
    class WSelectionChangedAction<T> : WUndoCommand
    {
		private readonly T[] m_removedEntity;
		private readonly T[] m_addedEntity;
		private readonly Selection<T> m_selection;

		public WSelectionChangedAction(Selection<T> parentSelection, T[] removedActors, T[] addedActors)
		{
			this.m_selection = parentSelection;
			m_removedEntity = removedActors != null ? removedActors : new T[0];
			m_addedEntity = addedActors != null ? addedActors : new T[0];
		}

        public override void Redo()
        {
            base.Redo();

			foreach(var entity in m_removedEntity)
			{
				m_selection.SelectedObjects.Remove(entity);
				//entity.IsSelected = false;
			}

			foreach(var entity in m_addedEntity)
			{
				m_selection.SelectedObjects.Add(entity);
				//entity.IsSelected = true;
			}
		}

        public override void Undo()
        {
            base.Undo();

			foreach (var entity in m_addedEntity)
			{
				m_selection.SelectedObjects.Remove(entity);
				//entity.IsSelected = false;
			}

			foreach (var entity in m_removedEntity)
			{
				m_selection.SelectedObjects.Add(entity);
				//entity.IsSelected = true;
			}
        }
    }
}
