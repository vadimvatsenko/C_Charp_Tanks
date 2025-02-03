using C_Charp_Tanks.Renderer;

namespace C_Charp_Tanks.Blocks;

public class DestructibleBlock: Block
{
    public DestructibleBlock(BlockType type, char symbol, Vector2 position) : base(type, symbol, position)
    {
        Color = 6;
    }
}