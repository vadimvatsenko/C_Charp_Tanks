using System.Collections;

namespace C_Charp_Tanks.Blocks;

public abstract class GameObjects<T> 
{
    protected List<T> Objects { get;} = new List<T>();
    public virtual void AddObject(T obj) => Objects.Add(obj);
    public virtual void RemoveObject(T obj) => Objects.Remove(obj);
    public virtual void Clear() => Objects.Clear();
    public IEnumerable<T> GetObjects() => Objects; // забрать все объекты
}