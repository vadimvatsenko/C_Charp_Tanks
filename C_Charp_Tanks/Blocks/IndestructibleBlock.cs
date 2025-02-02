using System.Drawing;
using C_Charp_Tanks.Renderer;

namespace C_Charp_Tanks.Blocks;

public class IndestructibleBlock : Block
{
    public IndestructibleBlock(BlockType type, char symbol, Vector2 position, IRenderer renderer) : base(type, symbol, position, renderer)
    {
        Color = 3;
    }
    
}