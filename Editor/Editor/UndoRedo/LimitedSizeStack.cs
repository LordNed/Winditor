using System.Collections.Generic;

namespace Editor
{
    /// <summary>
    /// A limited size stack, which stores the newest values first and oldest values last.
    /// </summary>
    class LimitedSizeStack<T> : LinkedList<T>
    {
        public int MaxSize { get { return m_maxSize; } }

        private int m_maxSize;

        public LimitedSizeStack()
        {
            m_maxSize = 50;
        }

        public void SetMaxSize(int maxSize)
        {
            m_maxSize = maxSize;

            // If we've been downsized, make sure we remove all of the old entries right away.
            while(Count > m_maxSize)
                RemoveLast();
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

        public T Peek()
        {
            if (First == null)
                return default(T);

            return First.Value;
        }
    }
}
