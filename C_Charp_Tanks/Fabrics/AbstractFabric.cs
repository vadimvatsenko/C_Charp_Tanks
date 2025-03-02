using C_Charp_Tanks.Blocks;

public abstract class AbstractFabric<T>
{
    public List<T> _list;

    public AbstractFabric()
    {
        _list = new List<T>();
    }
    public abstract event Action? OnItemsUpdated;
    public abstract void AddItem(T item);
    public abstract void RemoveItem(T item);
    public abstract void Clear();
    public abstract List<T> GetItems();
}