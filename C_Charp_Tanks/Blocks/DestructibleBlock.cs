using System.Collections.Immutable;
using C_Charp_Tanks.Renderer;

namespace C_Charp_Tanks.Blocks;

public class DestructibleBlock: Block
{
    public int Lives { get; private set; } = 2;
    public DestructibleBlock(BlockType type, char symbol, Vector2 position) : base(type, symbol, position)
    {
        Color = 5;
    }

    public void GetDamage()
    {
        Symbol = Symbols.BrockenWall;
        this.FillBlock();
        Lives--;
    }

    public override void Update(double deltaTime)
    {
        CheckBulletCollision();
        if (Lives == 1)
        {
            Symbol = Symbols.BrockenWall;
        }
        else if (Lives <= 0)
        {
            this.Destroy();
        }
    }

    private void CheckBulletCollision()
    {
        foreach (var bullet in BulletObjects.Instance.GetObjects())
        {
            if (this.Collider.IsColliding(bullet.Collider))
            {
                GetDamage();
            }
        }
        
    }

    public void Destroy()
    {
    }
    
    
}