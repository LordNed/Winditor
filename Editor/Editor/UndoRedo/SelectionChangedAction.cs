using System.ComponentModel;

namespace WindEditor
{
    class WSelectionChangedAction : WUndoCommand
    {
        WActorNode[] m_oldActorList;
        WActorNode[] m_newActorList;

        BindingList<WActorNode> m_selectionListRef; 

        public WSelectionChangedAction(WActorNode[] oldActorList, WActorNode[] newActorList, BindingList<WActorNode> selectionList)
        {
            m_oldActorList = oldActorList;
            m_newActorList = newActorList;
            m_selectionListRef = selectionList;
        }

        public override void Redo()
        {
            base.Redo();

            // ToDo: This raises a lot of events which kind of blasts the UI with events.
            m_selectionListRef.Clear();
            for (int i = 0; i < m_newActorList.Length; i++)
                m_selectionListRef.Add(m_newActorList[i]);

            // Update their selected states
            for (int i = 0; i < m_oldActorList.Length; i++)
                m_oldActorList[i].Flags &= ~ActorFlags.Selected;

            for (int i = 0; i < m_newActorList.Length; i++)
                m_newActorList[i].Flags &= ActorFlags.Selected;
        }

        public override void Undo()
        {
            base.Undo();

            m_selectionListRef.Clear();
            for (int i = 0; i < m_oldActorList.Length; i++)
                m_selectionListRef.Add(m_oldActorList[i]);

            // Update their selected states
            for (int i = 0; i < m_newActorList.Length; i++)
                m_newActorList[i].Flags &= ~ActorFlags.Selected;

            for (int i = 0; i < m_oldActorList.Length; i++)
                m_oldActorList[i].Flags &= ActorFlags.Selected;
        }

    }
}
