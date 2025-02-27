using System.Drawing;
using C_Charp_Tanks.Renderer;

namespace C_Charp_Tanks.Blocks;

public class IndestructibleBlock : Block
{
    public IndestructibleBlock(BlockType type, char symbol, Vector2 position) : base(type, symbol, position)
    {
        Color = 6;
    }

    public override void Update(double deltaTime)
    {
        
    }

    public override void GetDamage()
    {
        
    }
}