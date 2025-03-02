using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.Engine;
using  C_Charp_Tanks;
using C_Charp_Tanks.Fabrics;
using C_Charp_Tanks.Systems;
using C_Charp_Tanks.Venicals.Enemy;

namespace C_Charp_Tanks.Venicals;

public class Player : Unit, IShoot
{
    private readonly IConsoleInput _input;
    private readonly CollisionSystem _collisionSystem;
    
    private bool _canShoot = true;
    private double _currentShootTimer = 0;
    private const double CoolDownTime = 2.0;
    
    private List<Block> _blocks = new List<Block>();
    private List<Unit> _units = new List<Unit>();
    
    public Player(Vector2 position, FabricController fabricController,  IConsoleInput input, CollisionSystem collisionSystem) 
        : base(position, fabricController, collisionSystem)
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
   
    public override void Update(double deltaTime)
    {
        if (!_canShoot)
        {
            _currentShootTimer += deltaTime;
            if (_currentShootTimer >= CoolDownTime)
            {
                _canShoot = true;
            }
        }
        
        _blocks = _fabricController.BlocksFabric.GetItems().ToList();
        _units = _fabricController.UnitFabric.GetItems().ToList();
        
        
    }
   
    private void MoveUp()
    {
        View = PlayerData.Instance.TankUpView;
        CurrentDirection = Vector2.Up;
        
        if (_collisionSystem.CanMove(this, Vector2.Up))
        {
            Position += Vector2.Up;
        }
    }

    private void MoveDown()
    {
        View = PlayerData.Instance.TankDownView;
        CurrentDirection = Vector2.Down;
        if (_collisionSystem.CanMove(this, Vector2.Up))
        {
            Position += Vector2.Down;
        }
    }

    private void MoveLeft()
    {
        View = PlayerData.Instance.TankLeftView;
        CurrentDirection = Vector2.Left;
        if (_collisionSystem.CanMove(this, Vector2.Up))
        {
            Position += Vector2.Left;
        }
    }

    private void MoveRight()
    {
        View = PlayerData.Instance.TankRightView;
        CurrentDirection = Vector2.Right;
        if (_collisionSystem.CanMove(this, Vector2.Up))
        {
            Position += Vector2.Right;
        }
    }
    
    
    public void Shoot()
    {
        if (!_canShoot) return;
        
        Vector2 shellPosition = Position + CurrentDirection + Vector2.One;
        Vector2 shellDirection = CurrentDirection;
        
        _fabricController.BulletsFabric.CreateBullet(shellPosition, shellDirection);
        _canShoot = false;
        _currentShootTimer = 0;
    }
}