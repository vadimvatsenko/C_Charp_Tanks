using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.Venicals;

namespace C_Charp_Tanks.Fabrics.BlocksFactory;

public abstract class AbstractBlocksFactory
{
    protected List<Block> _blocks = new List<Block>();
    
    public abstract void CreateBlocks();
    public abstract void AddBlock(Block block);
    public abstract void RemoveBlock(Block block);
    public abstract List<Block> GetBlocks();
    public abstract void ClearBlocks();
}