using System.Collections.Generic;
using WindEditor.Collision;

namespace WindEditor
{
    class WSelectionChangedAction<T> : WUndoCommand
    {
		private readonly T[] m_removedEntities;
		private readonly T[] m_addedEntities;
		private readonly Selection<T> m_selection;

		public WSelectionChangedAction(Selection<T> parentSelection, T[] removedActors, T[] addedActors)
		{
			this.m_selection = parentSelection;
			m_removedEntities = removedActors != null ? removedActors : new T[0];
			m_addedEntities = addedActors != null ? addedActors : new T[0];
		}

        public override void Redo()
        {
            base.Redo();

			foreach(var entity in m_removedEntities)
			{
				m_selection.SelectedObjects.Remove(entity);

				if (entity is WDOMNode)
				{
					(entity as WDOMNode).IsSelected = false;
				} else if (entity is CollisionTriangle)
				{
					(entity as CollisionTriangle).Deselect();
				}
			}

			foreach(var entity in m_addedEntities)
			{
				m_selection.SelectedObjects.Add(entity);

				if (entity is WDOMNode)
				{
					(entity as WDOMNode).IsSelected = true;
				}
				else if (entity is CollisionTriangle)
				{
					(entity as CollisionTriangle).Select();
				}
			}
		}

        public override void Undo()
        {
            base.Undo();

			foreach (var entity in m_addedEntities)
			{
				m_selection.SelectedObjects.Remove(entity);

				if (entity is WDOMNode)
				{
					(entity as WDOMNode).IsSelected = false;
				}
				else if (entity is CollisionTriangle)
				{
					(entity as CollisionTriangle).Deselect();
				}
			}

			foreach (var entity in m_removedEntities)
			{
				m_selection.SelectedObjects.Add(entity);

				if (entity is WDOMNode)
				{
					(entity as WDOMNode).IsSelected = true;
				}
				else if (entity is CollisionTriangle)
				{
					(entity as CollisionTriangle).Select();
				}
			}
        }
    }
}
