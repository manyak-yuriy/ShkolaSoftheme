using System;

namespace _02_Iterator
{
    /// <summary>
    /// The 'ConcreteIterator' class
    /// </summary>
    class Iterator<T> : IAbstractIterator<T>
    {
        private readonly Collection<T> _collection;
        private int _current;
        private int _step = 1;

        // Constructor
        public Iterator(Collection<T> collection)
        {
            _collection = collection;
        }

        // Gets first item
        public T First()
        {
            _current = 0;
            return _collection[_current];
        }

        // Gets next item
        public T Next()
        {
            _current += _step;
            
            return IsDone? default(T) : _collection[_current];
            
        }

        // Gets or sets stepsize
        public int Step
        {
            get { return _step; }
            set { _step = value; }
        }

        // Gets current iterator item
        public T CurrentItem
        {
            get { return _collection[_current]; }
        }

        // Gets whether iteration is complete
        public bool IsDone
        {
            get { return _current >= _collection.Count; }
        }
    }
}