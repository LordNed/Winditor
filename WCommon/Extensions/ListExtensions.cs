using System;
using System.Collections.Generic;

namespace WindEditor
{
    public static class ListExtensions
    {
        public static void RemoveSwap<T>(this List<T> list, int index)
        {
            list[index] = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
        }

        public static void RemoveSwap<T>(this List<T> list, T item)
        {
            int index = list.IndexOf(item);
            RemoveSwap(list, index);
        }

        public static void RemoveSwap<T>(this List<T> list, Predicate<T> predicate)
        {
            int index = list.FindIndex(predicate);
            RemoveSwap(list, index);
        }
    }
}
