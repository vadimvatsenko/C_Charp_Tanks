using C_Charp_Tanks.Fabrics;
namespace C_Charp_Tanks.Blocks;

public class DestructibleBlock: Block
{
    public int Lives { get; private set; } = 2;
    public event Action? OnDestructionBlock;
    public DestructibleBlock(BlockType type, char symbol, Vector2 position) : base(type, symbol, position)
    {
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
            OnDestructionBlock?.Invoke();
        }
    }
}