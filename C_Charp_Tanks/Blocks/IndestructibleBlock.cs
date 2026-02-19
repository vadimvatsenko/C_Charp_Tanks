namespace C_Charp_Tanks.Blocks;

public class IndestructibleBlock : Block
{
    public IndestructibleBlock(BlockType type, char symbol, Vector2 position, char[,] layer) : base(type, symbol, position, layer)
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