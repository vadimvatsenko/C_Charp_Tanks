using System.Collections.Immutable;
using C_Charp_Tanks.Fabrics;
using C_Charp_Tanks.Renderer;

namespace C_Charp_Tanks.Blocks;

public class DestructibleBlock: Block
{
    private FabricController _fabricController;
    public int Lives { get; private set; } = 2;
    public DestructibleBlock(FabricController fabricController, BlockType type, char symbol, Vector2 position) 
        : base(type, symbol, position)
    {
        _fabricController = fabricController;
        Color = 5;
    }

    public override void GetDamage()
    {
        Symbol = Symbols.BrockenWall;
        FillBlock();
        Lives--;
    }

    public override void Update(double deltaTime)
    {
        if (Lives == 1)
        {
            Symbol = Symbols.BrockenWall;
        }
        else if (Lives <= 0)
        {
            this.Destroy();
        }
    }
    
    public void Destroy()
    {
        _fabricController.BlocksFabric.RemoveBlock(this);
    }
}