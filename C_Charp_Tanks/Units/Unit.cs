using System.Drawing;
using C_Charp_Tanks.Renderer;

namespace C_Charp_Tanks.Venicals;

public abstract class Unit : IUpdatable
{
    public Vector2 Position { get; set; }
    protected char[,] View  { get; private set; }
    
    private IRenderer _renderer;

    public Unit(Vector2 position, IRenderer renderer)
    {
        Position = position;
        _renderer = renderer;
        View = GameData.Instance.TankUpView;
    }
    
    public virtual bool TryMoveLeft() 
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
    }
    
    public void Update(double deltaTime)
    {
        
    }
}