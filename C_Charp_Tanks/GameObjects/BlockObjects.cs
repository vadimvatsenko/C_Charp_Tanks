namespace C_Charp_Tanks.Blocks;

public class BlockObjects : GameObjects<Block>
{
    public static BlockObjects Instance { get;} = new BlockObjects();

    private BlockObjects()
    {
        
    }
}