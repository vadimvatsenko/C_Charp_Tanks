using C_Charp_Tanks.Renderer;

namespace C_Charp_Tanks.Blocks;

public class DestructibleBlock: Block
{
    public DestructibleBlock(BlockType type, char symbol, Vector2 position, IRenderer renderer) : base(type, symbol, position, renderer)
    {
        Color = 6;
    }
}