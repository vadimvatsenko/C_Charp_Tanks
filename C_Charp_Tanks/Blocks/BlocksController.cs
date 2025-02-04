using C_Charp_Tanks.MazeGenerator;
using C_Charp_Tanks.Venicals;

namespace C_Charp_Tanks.Blocks;

public class BlocksController 
{
    public static List<Block> Blocks {get; private set;}
    
    public BlocksController()
    {
        Blocks = new List<Block>();
    }
    
    public void AddBlock(Block block)
    {
        Blocks.Add(block);
    }

    public void RemoveBlock(Block block)
    {
        Blocks.Remove(block);
    }

    public void Clear()
    {
        Blocks.Clear();
    }
}