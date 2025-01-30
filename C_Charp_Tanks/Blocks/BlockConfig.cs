namespace C_Charp_Tanks.Blocks;

public class BlockConfig
{
    public Vector2 Position { get; private set; }
    public char[] View { get; private set; }
    public BlockType Type { get; private set; }

    public BlockConfig(Vector2 position, char[] view, BlockType blockType)
    {
        Position = position;
        View = view;
        Type = blockType;
    }
}