using C_Charp_Tanks.Blocks;

public abstract class AbstractFabric<T>
{
    protected List<T> _list = new List<T>();
    public event Action? OnItemsUpdated;
    
    protected void AddItem(T item)
    {
        _list.Add(item);
        OnItemsUpdated?.Invoke();
    }
    
    public void RemoveItem(T item)
    {
        _list.Remove(item);
        OnItemsUpdated?.Invoke();
    }
    public List<T> GetItems() => _list;
    
    public void ClearItems()
    {
        _list.Clear();
    }
}