using C_Charp_Tanks.C_Charp_Tanks.Engine.Collider;
using C_Charp_Tanks.Engine.Renderer;
using C_Charp_Tanks.Fabrics;
using C_Charp_Tanks.Systems;
using C_Charp_Tanks.Venicals;

namespace C_Charp_Tanks.Units;

public abstract class Unit
{
    protected readonly FabricController FabricController;
    protected readonly CollisionSystem CollisionSystem;
    public UnitType UnitType { get; protected set; }
    public Vector2 Position { get; protected set; }
    public Vector2 CurrentDirection { get; protected set; }
    public BoxCollider2D Collider { get; protected set; }
    public float Speed { get; protected set; }
    public char[,] View  { get; protected set; }
    public int Health { get; protected set; } = 100;
    
    public char[,] Layer { get; protected set; }
    
    public Unit(Vector2 position, FabricController fabricController, CollisionSystem collisionSystem, char[,] layer)
    {
        CollisionSystem = collisionSystem;
        FabricController = fabricController;
        Layer = layer;
        
        Position = position;
        Collider = new BoxCollider2D(position, new Vector2(3, 3));
        View = PlayerData.Instance.TankUpView;
    }
    
    public virtual void Update(double deltaTime)
    {
        UpdateCollider();
    }
    
    public virtual void Render(BaseRenderer renderer)
    {
        for (int x = 0; x < View.GetLength(0); x++)
        {
            for (int y = 0; y < View.GetLength(1); y++)
            {
                renderer.DrawChar( Layer,x + Position.X, y + Position.Y, View[x, y]);
            }
        }
    }

    public void GetDamage(int damage)
    {
        Health -= damage;
        if (damage >= Health)
        {
            Health = 0;
        }
    }
    
    private void UpdateCollider()
    {
        Collider.Position = Position;
    }
}
