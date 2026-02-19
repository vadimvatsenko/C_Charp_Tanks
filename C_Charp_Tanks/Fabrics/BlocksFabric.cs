using C_Charp_Tanks.Blocks;

namespace C_Charp_Tanks.Fabrics.BlocksFactory;

public class BlocksFabric : AbstractFabric<Block>
{
    public override event Action? OnItemsUpdated;

    public BlocksFabric()
    {
        _list = new List<Block>();
    }
    public override void AddItem(Block item)
    {
        _list.Add(item);
        OnItemsUpdated?.Invoke();
    }

    public override void RemoveItem(Block item)
    {
        _list.Remove(item);
        OnItemsUpdated?.Invoke();
    }

    public override void Clear() => _list.Clear();
    public override List<Block> GetItems() => _list;
    
}