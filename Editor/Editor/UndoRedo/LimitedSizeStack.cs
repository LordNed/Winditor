using System.Collections.Generic;

namespace Editor
{
    class LimitedSizeStack<T> : LinkedList<T>
    {
        private int m_maxSize;

        public LimitedSizeStack()
        {
            m_maxSize = 50;
        }

        public void SetMaxSize(int maxSize)
        {
            m_maxSize = maxSize;
        }

        public void Push(T item)
        {
            AddFirst(item);

            if (Count > m_maxSize)
                RemoveLast();
        }

        public T Pop()
        {
            var item = First.Value;
            RemoveFirst();
            return item;
        }
    }
}
