using C_Charp_Tanks.Blocks;

public abstract class AbstractFabric<T>
{
    protected List<T> _list = new List<T>();
    public abstract event Action<T>? OnItemDestroyed;
    public abstract event Action? OnItemCreated;
    
    public abstract void CreateItem();
    public abstract void AddItem(T item);
    public abstract void RemoveItem(T item);
    public abstract List<T> GetItem();
    public abstract void ClearItem();
}