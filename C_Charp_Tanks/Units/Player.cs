using System.Drawing;
using C_Charp_Tanks.Renderer;

namespace C_Charp_Tanks.Venicals;

public class Player : Unit, IUpdatable
{
    public event Action OnDeath;
    
    private IConsoleInput _input;
    private IRenderer _renderer;

    private char[,] _currentView;
    private Direction _currentDirection = Direction.Up;
    
    public Player(Vector2 position, IRenderer renderer, IConsoleInput input) : base(position, renderer)
    {
       _input = input;
       _renderer = renderer;
       _currentView = View;
       
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
        _currentView = RotateView(_currentView, Direction.Up);
    }

    private void MoveDown()
    {
        //TryMoveDown();
        Position += Vector2.Down;
        if(_currentDirection == Direction.Down) return;
        _currentView = RotateView(_currentView, Direction.Down);
    }

    private void MoveLeft()
    {
       // TryMoveLeft();
       Position += Vector2.Left;
       if(_currentDirection == Direction.Left) return;
       _currentView = RotateView(_currentView, Direction.Left);
    }

    private void MoveRight()
    {
        //TryMoveRight();
        Position += Vector2.Right;
        if(_currentDirection == Direction.Right) return;
        _currentView = RotateView(_currentView, Direction.Right);
    }

    private char[,] RotateView(char[,] view, Direction direction)
    {
        /*int rows = view.GetLength(0);
        int cols = view.GetLength(1);
        
        
        char[,] rotated = new char[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                rotated[j, rows - 1 - i] = view[i, j];
            }
        }
        return rotated;*/
        
        _currentDirection = direction;
        int rows = view.GetLength(0);
        int cols = view.GetLength(1);
        char[,] rotated;

        if (direction == Direction.Right)
        {
            
            // Вращение на 90 градусов по часовой стрелке
            rotated = new char[cols, rows];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    rotated[j, rows - 1 - i] = view[i, j];
                }
            }
        }
        else if (direction == Direction.Left)
        {
            // Вращение на 90 градусов против часовой стрелки
            rotated = new char[cols, rows];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    rotated[cols - 1 - j, i] = view[i, j];
                }
            }
        }
        else if (direction == Direction.Up)
        {
            // Зеркальное отражение по горизонтали
            rotated = new char[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    rotated[i, j] = view[rows - 1 - i, j];
                }
            }
        }
        else if (direction == Direction.Down)
        {
            // Зеркальное отражение по вертикали
            rotated = new char[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    rotated[i, j] = view[i, cols - 1 - j];
                }
            }
            
        }
        else
        {
            // Если направление не указано, вернуть исходное представление
            rotated = view;
        }

        return rotated;
    }

    public void Update(double deltaTime)
    {
        _renderer.Clear();
        for (int x = 0; x < _currentView.GetLength(0); x++)
        {
            for (int y = 0; y < _currentView.GetLength(0); y++)
            {
                _renderer.SetPixel(x + Position.X,y + Position.Y, _currentView[x, y], 5);
            }
        }
        
        _renderer.Render();
    }
}