using C_Charp_Tanks.Venicals;

namespace C_Charp_Tanks.Fabrics.BulletsFactory;

public class BulletsFabric : AbstractFabric<Ammunition>
{
    public override event Action<Ammunition>? OnItemDestroyed;
    public override event Action? OnItemCreated;
    
    
    public BulletsFabric()
    {
        OnItemCreated += CreateItem;
        OnItemDestroyed += RemoveItem;
    }

    ~BulletsFabric()
    {
        OnItemCreated -= CreateItem;
        OnItemDestroyed -= RemoveItem;
    }
    
    public override void CreateItem()
    {
        
    }
    
    public void CreateAmmunition(FabricController fabricController, Vector2 position, Vector2 direction)
    {
        Bullet bullet = new Bullet(position, direction);
        AddItem(bullet);
    }

    public override void AddItem(Ammunition item) => _list.Add(item);
    public override void RemoveItem(Ammunition item) => _list.Remove(item);
    public override List<Ammunition> GetItem() => _list;
    public override void ClearItem() => _list.Clear();
    
}