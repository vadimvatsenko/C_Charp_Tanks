using C_Charp_Tanks.Fabrics;
namespace C_Charp_Tanks.Blocks;

public class DestructibleBlock: Block
{
    private readonly FabricController _fabricController;
    private int _lives = 2;
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
        _lives--;
    }

    public override void Update(double deltaTime)
    {
        if (_lives == 1)
        {
            Symbol = Symbols.BrockenWall;
        }
        else if (_lives <= 0)
        {
            this.Destroy();
        }
    }
    
    public void Destroy()
    {
        _fabricController.BlocksFabric.RemoveItem(this);
    }
}