using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BoardCore.GameCore.Utils
{
    /// <summary>
    /// 一般用于轮换先手以及顺序执行某些操作的时候使用
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjectEnumerator<T>
    {
        private IEnumerable<T> iter;
        private readonly LinkedList<T> list;

        public ObjectEnumerator(LinkedList<T> linkedList)
        {
            list = linkedList;
        }

        public T GetFirstInitial()
        {
            iter.GetEnumerator().Reset();
            return iter.FirstOrDefault();
        }

        public T GetFirstInitial(Func<T, bool> predicate)
        {
            iter.GetEnumerator().Reset();
            return iter.FirstOrDefault(predicate);
        }

        public T GetRandomInitial()
        {
            iter = list;
            iter.Skip(new Random().Next(list.Count - 1));
            return iter.FirstOrDefault();
        }

        public T GetRandomInitial(Func<T, bool> predicate)
        {
            iter = list;
            iter.Skip(new Random().Next(list.Count - 1));
            return iter.FirstOrDefault(predicate);
        }

        public T NextWhen(Func<T, bool> predicate)
        {
            T next = iter.FirstOrDefault(predicate);
            if (next == null)
            {
                iter.GetEnumerator().Reset();
                return iter.FirstOrDefault(predicate);
            }
            return next;
        }
    }
}
