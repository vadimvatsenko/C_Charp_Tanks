using C_Charp_Tanks.Engine;

namespace C_Charp_Tanks.Blocks;

public class EmptyBlock : Block
{
    public EmptyBlock(BlockType type, char symbol, Vector2 position) : base(type, symbol, position)
    {
        this.Type = BlockType.Empty;
        this.Symbol = ' ';
        this.Position = position;
        Collider = new BoxCollider2D(Position, Vector2.Zero);
    }

    public override void Update(double deltaTime)
    {
        
    }

    public override void GetDamage()
    {
        
    }
}