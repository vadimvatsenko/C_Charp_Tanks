using System.Drawing;
using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.Engine;
using C_Charp_Tanks.Renderer;

namespace C_Charp_Tanks.Venicals;

public class Player : Unit, IDisposable
{
    private readonly IConsoleInput _input;
    
    private Direction _currentDirection = Direction.Up;
    
    public Player(Vector2 position, IConsoleInput input) : base(position)
    {
       _input = input;
       Collider = new BoxCollider2D(position, new Vector2(3, 3));
       
       _input.MoveUp += MoveUp;
       _input.MoveDown += MoveDown;
       _input.MoveLeft += MoveLeft;
       _input.MoveRight += MoveRight;
    }
    
    public void Dispose()
    {
        _input.MoveUp -= MoveUp;
        _input.MoveDown -= MoveDown;
        _input.MoveLeft -= MoveLeft;
        _input.MoveRight -= MoveRight;
    }
    

    private void MoveUp()
    {
        View = GameData.Instance.TankUpView;
        if (!TryToMove(Vector2.Up))
        {
            Position += Vector2.Up;
        }
        
    }

    private void MoveDown()
    {
        View = GameData.Instance.TankDownView;
        if (!TryToMove(Vector2.Down))
        {
            Position += Vector2.Down;
        }
    }

    private void MoveLeft()
    {
        View = GameData.Instance.TankLeftView;
        if (!TryToMove(Vector2.Left))
        {
            Position += Vector2.Left;
        }
    }

    private void MoveRight()
    {
        View = GameData.Instance.TankRightView;
        if (!TryToMove(Vector2.Right))
        {
            Position += Vector2.Right;
        }
    }
    
    
    public override void Update(double deltaTime)
    {
        base.Update(deltaTime);
        UpdateCollider();
    }
    
}