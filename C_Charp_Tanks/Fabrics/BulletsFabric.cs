using C_Charp_Tanks.Venicals;

namespace C_Charp_Tanks.Fabrics.BulletsFactory;

public class BulletsFabric : AbstractFabric<Ammunition>
{
    public void CreateBullet(Vector2 position, Vector2 direction)
    {
        Bullet bullet = new Bullet(position, direction);
        AddItem(bullet);
    }

    public override event Action? OnItemsUpdated;
    public override void AddItem(Ammunition item)
    {
        _list.Add(item);
        OnItemsUpdated?.Invoke();
    }

    public override void RemoveItem(Ammunition item)
    {
        _list.Remove(item);
        OnItemsUpdated?.Invoke();
    }

    public override void Clear()
    {
        _list.Clear();
        OnItemsUpdated?.Invoke();
    }

    public override List<Ammunition> GetItems() => _list;

}