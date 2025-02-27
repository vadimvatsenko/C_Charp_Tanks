using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.Engine;
using  C_Charp_Tanks;
using C_Charp_Tanks.Fabrics;
using C_Charp_Tanks.Venicals.Enemy;

namespace C_Charp_Tanks.Venicals;

public class Player : Unit, IShoot
{
    private readonly IConsoleInput _input;
    
    private float _shootCooldown = 2f;
    private float _currentShootTimer = 0f;
    
    public Player(Vector2 position, FabricController fabricController,  IConsoleInput input) 
        : base(position, fabricController)
    {
        UnitType = UnitType.Player;
        
        _input = input;
        Collider = new BoxCollider2D(position, new Vector2(3, 3));
        CurrentDirection = Vector2.Up;
        
        _input.MoveUp += MoveUp;
        _input.MoveDown += MoveDown;
        _input.MoveLeft += MoveLeft;
        _input.MoveRight += MoveRight;
        _input.Shoot += Shoot;
    }

   ~Player()
    {
        _input.MoveUp -= MoveUp;
        _input.MoveDown -= MoveDown;
        _input.MoveLeft -= MoveLeft;
        _input.MoveRight -= MoveRight;
        _input.Shoot -= Shoot;
    }
   
    private void MoveUp()
    {
        View = PlayerData.Instance.TankUpView;
        CurrentDirection = Vector2.Up;
        
        if (TryToMove(Vector2.Up))
        {
            Position += Vector2.Up;
        }
    }

    private void MoveDown()
    {
        View = PlayerData.Instance.TankDownView;
        CurrentDirection = Vector2.Down;
        if (TryToMove(Vector2.Down))
        {
            Position += Vector2.Down;
        }
    }

    private void MoveLeft()
    {
        View = PlayerData.Instance.TankLeftView;
        CurrentDirection = Vector2.Left;
        if (TryToMove(Vector2.Left))
        {
            Position += Vector2.Left;
        }
    }

    private void MoveRight()
    {
        View = PlayerData.Instance.TankRightView;
        CurrentDirection = Vector2.Right;
        if (TryToMove(Vector2.Right))
        {
            Position += Vector2.Right;
        }
    }
    
    public override void Update(double deltaTime)
    {
        base.Update(deltaTime);
        if (_currentShootTimer > 0f)
        {
            _currentShootTimer -= (float)deltaTime;
        }
    }
    
    private bool TryToMove(Vector2 direction)
    {
        var blocks = _fabricController.BlocksFabric.GetItem().ToList();
        var units = _fabricController.UnitFabric.GetItem().ToList();
        
        Vector2 newPosition = Position + direction; // Новая позиция после движения
        BoxCollider2D newCollider = new BoxCollider2D(newPosition, Collider.Size); // Создаём временный коллайдер
        
        foreach (var block in blocks)
        {
            if (newCollider.IsColliding(block.Collider)) // Проверяем столкновение
                return false; // Есть столкновение — нельзя двигаться
        }

        foreach (var unit in units)
        {
            if (!(unit is Player) && newCollider.IsColliding(unit.Collider)) // Проверяем столкновение
                return false; // Есть столкновение — нельзя двигаться
        }

        return true; // Нет столкновения — можно двигаться
    }
    
    public void Shoot()
    {
        if (_currentShootTimer > 0f)
        {
            return;
        }
        
        Vector2 shellPosition = Position + CurrentDirection + Vector2.One;
        Vector2 shellDirection = CurrentDirection;
        
        _fabricController.ShellsFabric.CreateShell(
            _fabricController, 
            shellPosition, 
            shellDirection
        );
        
        _currentShootTimer = _shootCooldown;
    }
}