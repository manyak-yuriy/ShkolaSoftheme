using System.Collections;
using System.Collections.Generic;

namespace _02_Iterator
{
    /// <summary>
    /// The 'ConcreteAggregate' class
    /// </summary>
    class Collection<T> : IAbstractCollection<T>
    {
        private readonly List<T> _items = new List<T>();

        public Iterator<T> CreateIterator()
        {
            return new Iterator<T>(this);
        }

        // Gets item count
        public int Count
        {
            get { return _items.Count; }
        }

        // Indexer
        public T this[int index]
        {
            get { return _items[index]; }
            set { _items.Add(value); }
        }
    }
}