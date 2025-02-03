using System.Drawing;
using C_Charp_Tanks.Engine;
using C_Charp_Tanks.Renderer;

namespace C_Charp_Tanks.Venicals;

public abstract class Unit : IUpdatable
{
    public Vector2 Position { get; protected set; }
    public float Speed { get; protected set; }
    public BoxCollider2D Collider { get; protected set; }
    public char[,] View  { get; protected set; }
    public int Health { get; protected set; } = 100;
    
    public Unit(Vector2 position)
    {
        Position = position;
        Collider = new BoxCollider2D(position, new Vector2(3, 3));
        View = GameData.Instance.TankUpView;
    }

    
    public event Action OnDeath;
    
    /*public virtual bool TryMoveLeft() 
    {
        return TryChangePosition(Vector2.Left);
    }
    
    public virtual bool TryMoveRight() 
    {
        return TryChangePosition(Vector2.Right);
    }

    public virtual bool  TryMoveUp() 
    {
        return TryChangePosition(Vector2.Up);
    }

    public virtual bool TryMoveDown() 
    {
        return TryChangePosition(Vector2.Down);
    }
    
    private bool TryChangePosition(Vector2 newPosition)
    {
        Position = newPosition;
        return true;
    }*/
    
    public void Update(double deltaTime)
    {
        Collider.Position = Position;
    }
}