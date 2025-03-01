using C_Charp_Tanks.Blocks;

namespace C_Charp_Tanks.Fabrics.BlocksFactory;

public class BlocksFabric : AbstractFabric<Block>
{
    public void CreateBlock(Block item)
    {
        AddItem(item);
    }
}