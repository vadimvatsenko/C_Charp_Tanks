using System.Drawing;
using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.Engine;
using C_Charp_Tanks.Renderer;

namespace C_Charp_Tanks.Venicals;

public class Player : Unit, IUpdatable
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

    ~Player()
    {
        _input.MoveUp -= MoveUp;
        _input.MoveDown -= MoveDown;
        _input.MoveLeft -= MoveLeft;
        _input.MoveRight -= MoveRight;
    }

    private void MoveUp()
    {
        Console.WriteLine($"X = {Position.X}, Y = {Position.Y}");
        View = GameData.Instance.TankUpView;
        Position += Vector2.Up;
        Console.WriteLine(Position.X + "," + Position.Y);
    }

    private void MoveDown()
    {
        Console.WriteLine($"X = {Position.X}, Y = {Position.Y}");
        View = GameData.Instance.TankDownView;
        Position += Vector2.Down;
        Console.WriteLine(Position.X + "," + Position.Y);
    }

    private void MoveLeft()
    {
        Console.WriteLine($"X = {Position.X}, Y = {Position.Y}");
        View = GameData.Instance.TankLeftView;
       Position += Vector2.Left;
       Console.WriteLine(Position.X + "," + Position.Y);
    }

    private void MoveRight()
    {
        Console.WriteLine($"X = {Position.X}, Y = {Position.Y}");
        View = GameData.Instance.TankRightView;

        bool isMove = TryToMove(Vector2.Right);
        if (isMove)
        {
            Position += Vector2.Right;
            Console.WriteLine(Position.X + "," + Position.Y);
        }
        
    }

    private bool TryToMove(Vector2 direction)
    {
        foreach (var block in BlocksController.Blocks)
        {
            if(this.Collider.IsColliding(block.Collider))
                return true;
        }
        
        return false;
    }

    private void UpdateCollider()
    {
        Collider.Position = Position;
    }
    
    public void Update(double deltaTime)
    {
        UpdateCollider();
    }

    public void RenderPlayer(IRenderer renderer)
    {
        UpdateCollider();
        for (int x = 0; x < View.GetLength(0); x++)
        {
            for (int y = 0; y < View.GetLength(0); y++)
            {
                renderer.SetPixel(x + Position.X, y + Position.Y, View[x, y], 2);
            }
        }
    }
}