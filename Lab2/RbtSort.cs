using System;
using System.Linq;

namespace Lab2
{
    public class RbtSort<T> where T : IComparable<T>
    {
        private int _count;

        public int GetCount()
        {
            return _count;
        }

        public T[] Sort(T[] arr)
        {
            if (arr == null)
                throw new ArgumentNullException();
            if (arr.Length == 0)
            {
                return arr;
            }

            var rbt = new RedBlackTree<T>();
            foreach (var item in arr)
            {
                rbt.Insert(item);
            }

            var res = rbt.InOrder();
            _count = rbt.GetCount();
            return res.ToArray();
        }
    }
}