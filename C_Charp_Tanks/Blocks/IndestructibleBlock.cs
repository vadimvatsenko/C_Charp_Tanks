using System.Drawing;
using C_Charp_Tanks.Renderer;

namespace C_Charp_Tanks.Blocks;

public class IndestructibleBlock : Block, IUpdatable
{
    public IndestructibleBlock(BlockType type, char symbol) : base(type, symbol)
    {
    }

    public void Update(double deltaTime)
    {
        throw new NotImplementedException();
    }
}