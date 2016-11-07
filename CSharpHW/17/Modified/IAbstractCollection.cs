namespace _02_Iterator
{
    /// <summary>
    /// The 'Aggregate' interface
    /// </summary>
    interface IAbstractCollection<T>
    {
        Iterator<T> CreateIterator();
    }
}