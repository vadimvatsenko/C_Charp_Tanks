using System.Drawing;
using C_Charp_Tanks.Engine;
using C_Charp_Tanks.Renderer;

namespace C_Charp_Tanks.Venicals;

public class Player : Unit, IUpdatable
{
    public event Action OnDeath;
    
    private readonly IConsoleInput _input;
    private readonly IRenderer _renderer;
    public BoxCollider2D Collider { get; private set; }

    public char[,] CurrentView { get; private set; }
    
    private Direction _currentDirection = Direction.Up;
    
    public Player(Vector2 position, IRenderer renderer, IConsoleInput input) : base(position, renderer)
    {
       _input = input;
       _renderer = renderer;
       Collider = new BoxCollider2D(position, new Vector2(3, 3));
       CurrentView = View;
       
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
        //TryMoveUp();
        
        CurrentView = GameData.Instance.TankUpView;
        Position += Vector2.Up;
    }

    private void MoveDown()
    {
        //TryMoveDown();
        
        CurrentView = GameData.Instance.TankDownView;
        Position += Vector2.Down;
    }

    private void MoveLeft()
    {
       // TryMoveLeft();
       
       CurrentView = GameData.Instance.TankLeftView;
       Position += Vector2.Left;
    }

    private void MoveRight()
    {
        //TryMoveRight();
        
        CurrentView = GameData.Instance.TankRightView;
        Position += Vector2.Right;
    }

    private void UpdateCollider()
    {
        Collider.Position = Position;
    }
    
    public void Update(double deltaTime)
    {
        UpdateCollider();
        //_renderer.Clear();
        for (int x = 0; x < CurrentView.GetLength(0); x++)
        {
            for (int y = 0; y < CurrentView.GetLength(0); y++)
            {
                _renderer.SetPixel(x + Position.X, y + Position.Y, CurrentView[x, y], 2);
            }
        }
        
        //_renderer.Render();
    }

    public void Draw()
    {
        UpdateCollider();
        _renderer.Clear();
        for (int x = 0; x < CurrentView.GetLength(0); x++)
        {
            for (int y = 0; y < CurrentView.GetLength(0); y++)
            {
                _renderer.SetPixel(x + Position.X, y + Position.Y, CurrentView[x, y], 2);
            }
        }
        
        _renderer.Render();
    }
    
    

}