using System.Drawing;
using C_Charp_Tanks.Engine;
using C_Charp_Tanks.Renderer;

namespace C_Charp_Tanks.Venicals;

public class Player : Unit, IUpdatable
{
    public event Action OnDeath;
    
    private IConsoleInput _input;
    private IRenderer _renderer;
    public BoxCollider2D _collider { get; private set; }

    public char[,] CurrentView { get; private set; }
    private Direction _currentDirection = Direction.Up;
    
    public Player(Vector2 position, IRenderer renderer, IConsoleInput input) : base(position, renderer)
    {
       _input = input;
       _renderer = renderer;
       _collider = new BoxCollider2D(position, new Vector2(3, 3));
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
        Position += Vector2.Up;
        if(_currentDirection == Direction.Up) return;
        CurrentView = RotateView(CurrentView, Direction.Up);
    }

    private void MoveDown()
    {
        //TryMoveDown();
        Position += Vector2.Down;
        if(_currentDirection == Direction.Down) return;
        CurrentView = RotateView(CurrentView, Direction.Down);
    }

    private void MoveLeft()
    {
       // TryMoveLeft();
       Position += Vector2.Left;
       if(_currentDirection == Direction.Left) return;
       CurrentView = RotateView(CurrentView, Direction.Left);
    }

    private void MoveRight()
    {
        //TryMoveRight();
        Position += Vector2.Right;
        if(_currentDirection == Direction.Right) return;
        CurrentView = RotateView(CurrentView, Direction.Right);
    }

    private void UpdateCollider()
    {
        _collider.Position = Position;
    }

    private char[,] RotateView(char[,] view, Direction direction)
    {
        _currentDirection = direction;
        int rows = view.GetLength(0);
        int cols = view.GetLength(1);
        char[,] shifted = new char[rows, cols];

        if (direction == Direction.Right)
        {
            // Сдвиг вправо
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    shifted[i, j - 1] = view[i, j];
                }
            }
        }
        
        return shifted;
    }

    public void Update(double deltaTime)
    {
        UpdateCollider();
        _renderer.Clear();
        for (int x = 0; x < CurrentView.GetLength(0); x++)
        {
            for (int y = 0; y < CurrentView.GetLength(0); y++)
            {
                _renderer.SetPixel(x + Position.X,y + Position.Y, CurrentView[x, y], 5);
            }
        }
        
        _renderer.Render();
    }
}