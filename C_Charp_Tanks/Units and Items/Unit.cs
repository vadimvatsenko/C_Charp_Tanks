using System.Drawing;
using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.Engine;
using C_Charp_Tanks.Fabrics;
using C_Charp_Tanks.Renderer;

namespace C_Charp_Tanks.Venicals;

public abstract class Unit
{
    protected FabricController _fabricController;
    public UnitType UnitType { get; protected set; }
    public Vector2 Position { get; protected set; }
    public Vector2 CurrentDirection { get; protected set; }
    public BoxCollider2D Collider { get; protected set; }
    public float Speed { get; protected set; }
    public char[,] View  { get; protected set; }
    public int Health { get; protected set; } = 100;
    
    // Время между выстрелами (2 секунды)
    private float _shootCooldown = 2f;
    // Текущее время до готовности следующего выстрела
    private float _currentShootTimer = 0f;
    
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
        if (damage >= Health)
        {
            Health = 0;
        }
    }
    
    protected void UpdateCollider()
    {
        Collider.Position = Position;
    }

    public virtual void Destroy()
    {
        _fabricController.UnitFabric.RemoveItem(this);
    }
}
