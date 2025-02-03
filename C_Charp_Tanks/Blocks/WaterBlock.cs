using C_Charp_Tanks.Renderer;

namespace C_Charp_Tanks.Blocks;

public class WaterBlock : Block
{
    public WaterBlock(BlockType type, char symbol, Vector2 position) : base(type, symbol, position)
    {
        Color = 1;
    }
}