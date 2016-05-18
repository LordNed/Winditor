using System.ComponentModel;

namespace WindEditor
{
    class WSelectionChangedAction : WUndoCommand
    {
        WMapActor[] m_oldActorList;
        WMapActor[] m_newActorList;

        BindingList<WMapActor> m_selectionListRef; 

        public WSelectionChangedAction(WMapActor[] oldActorList, WMapActor[] newActorList, BindingList<WMapActor> selectionList)
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
        }

        public override void Undo()
        {
            base.Undo();

            m_selectionListRef.Clear();
            for (int i = 0; i < m_oldActorList.Length; i++)
                m_selectionListRef.Add(m_oldActorList[i]);
        }
    }
}
