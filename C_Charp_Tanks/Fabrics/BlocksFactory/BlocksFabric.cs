using C_Charp_Tanks.Blocks;

namespace C_Charp_Tanks.Fabrics.BlocksFactory;

public class BlocksFabric : AbstractBlocksFactory
{
    public override void CreateBlocks()
    {
    }

    public override void AddBlock(Block block) => _blocks.Add(block);
    
    public override void RemoveBlock(Block block) => _blocks.Remove(block);
    
    public override List<Block> GetBlocks() => _blocks;
   
}