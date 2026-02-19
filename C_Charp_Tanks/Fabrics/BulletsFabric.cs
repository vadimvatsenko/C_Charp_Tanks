using C_Charp_Tanks.Items.Shells;

namespace C_Charp_Tanks.Fabrics;

public class BulletsFabric : AbstractFabric<Ammunition>
{
    private char[,] _bulletsLayer;

    public BulletsFabric(char[,] bulletsLayer)
    {
        _bulletsLayer = bulletsLayer;
    }
    
    public void CreateBullet(Vector2 position, Vector2 direction)
    {
        Bullet bullet = new Bullet(position, direction,  _bulletsLayer);
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