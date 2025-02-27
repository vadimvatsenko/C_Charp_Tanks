using C_Charp_Tanks.Blocks;

namespace C_Charp_Tanks.Fabrics.BlocksFactory;

public class BlocksFabric : AbstractFabric<Block>
{
    public override event Action<Block>? OnItemDestroyed;
    public override event Action? OnItemCreated;
    public BlocksFabric()
    {
        OnItemCreated += CreateItem;
        OnItemDestroyed += RemoveItem;
    }

    ~BlocksFabric()
    {
        OnItemCreated -= CreateItem;
        OnItemDestroyed -= RemoveItem;
    }
    
    public override void CreateItem() => _list = new List<Block>();
    public override void AddItem(Block item) => _list.Add(item);
    public override void RemoveItem(Block item) => _list.Remove(item);
    public override List<Block> GetItem() => _list;
    public override void ClearItem() => _list.Clear();
}