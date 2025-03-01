using C_Charp_Tanks.Fabrics;
namespace C_Charp_Tanks.Blocks;

public class DestructibleBlock: Block
{
    private int _lives = 2;
    public event Action? OnDestructionBlock;
    public DestructibleBlock(BlockType type, char symbol, Vector2 position) : base(type, symbol, position)
    {
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
            OnDestructionBlock?.Invoke();
        }
    }
}