namespace _02_Iterator
{
    interface IAbstractIterator<T>
    {
        T First();
        
        T Next();
        
        bool IsDone { get; }
        
        T CurrentItem { get; }
    }
}