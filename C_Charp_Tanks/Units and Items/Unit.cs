using System.Drawing;
using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.Engine;
using C_Charp_Tanks.Fabrics;
using C_Charp_Tanks.Renderer;

namespace C_Charp_Tanks.Venicals;

public abstract class Unit
{
    public Vector2 Position { get; protected set; }
    public BoxCollider2D Collider { get; protected set; }
    public float Speed { get; protected set; }
    public char[,] View  { get; protected set; }
    public int Health { get; protected set; } = 100;
    public Vector2 CurrentDirection { get; protected set; }
    protected FabricController _fabricController;
    
    public Unit(Vector2 position, FabricController fabricController)
    {
        _fabricController = fabricController;
        Position = position;
        Collider = new BoxCollider2D(position, new Vector2(3, 3));
        View = PlayerData.Instance.TankUpView;
    }
    
    public virtual void Update(double deltaTime)
    {
        UpdateCollider();
    }
    
    // Метод проверки столкновения с блоками
    public virtual bool TryToMove(Vector2 direction)
    {
        
        
        Vector2 newPosition = Position + direction; // Новая позиция после движения
        BoxCollider2D newCollider = new BoxCollider2D(newPosition, Collider.Size); // Создаём временный коллайдер

        foreach (var block in _fabricController.BlocksFabric.GetBlocks())
        {
            if (newCollider.IsColliding(block.Collider)) // Проверяем столкновение
                return false; // Есть столкновение — нельзя двигаться
        }

        foreach (var unit in _fabricController.UnitFabric.GetUnits())
        {
            if (!(unit is Player) && newCollider.IsColliding(unit.Collider)) // Проверяем столкновение
                return false; // Есть столкновение — нельзя двигаться
        }

        return true; // Нет столкновения — можно двигаться
    }
    
    public virtual void Render(IRenderer renderer)
    {
        UpdateCollider();
        for (int x = 0; x < View.GetLength(0); x++)
        {
            for (int y = 0; y < View.GetLength(1); y++)
            {
                renderer.SetPixel(x + Position.X, y + Position.Y, View[x, y], this is Player? (byte)2 : (byte)3);
            }
        }
    }

    public virtual void GetDamage(int damage)
    {
        Health -= damage;
    }
    
    protected void UpdateCollider()
    {
        Collider.Position = Position;
    }

    public virtual void Destroy()
    {
        _fabricController.UnitFabric.RemoveUnit(this);
    }
}
