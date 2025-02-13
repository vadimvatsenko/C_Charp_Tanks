using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.Engine;
using C_Charp_Tanks.Fabrics;
using C_Charp_Tanks.Renderer;

namespace C_Charp_Tanks.Venicals;

public class Bullet : Unit
{
    private char _view;
    private Vector2 _direction;
    public bool IsDestroyed {get; private set;}
    public Bullet(Vector2 position, FabricController fabricController, Vector2 direction) 
        : base(position, fabricController)
    {
        Collider = new BoxCollider2D(position, new Vector2(1, 1));
        _view = Symbols.Wall;
        _direction = direction;
    }
    public override void Render(IRenderer renderer)
    {
        renderer.SetPixel(Position.X, Position.Y, _view, 4);
    }

    public override void Update(double deltaTime)
    {
        bool isWalkable = TryToMove(_direction);
        if (isWalkable)
        {
            Position += _direction;
        }
    }

    public override bool TryToMove(Vector2 direction)
    {
        Vector2 newPosition = Position + direction;
        BoxCollider2D newCollider = new BoxCollider2D(newPosition, Collider.Size);

        foreach (var block in _fabricController.BlocksFabric.GetBlocks())
        {
            if (newCollider.IsColliding(block.Collider)
                && block.Type != BlockType.Water)
            {
                Destroy();
                return false;
            }
            
        }

        foreach (var unit in _fabricController.UnitFabric.GetUnits())
        {
            if (newCollider.IsColliding(unit.Collider))
            {
                Destroy();
                return false;
            }
        }

        return true; // Если нет столкновений, можно двигаться
    }
    
    public void Destroy()
    {
        IsDestroyed = true;
        _fabricController.BulletsFabric.RemoveBullet(this);
    }
}