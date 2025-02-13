using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.Engine;
using  C_Charp_Tanks;
using C_Charp_Tanks.Fabrics;

namespace C_Charp_Tanks.Venicals;

public class Player : Unit
{
    private readonly IConsoleInput _input;
    private GameObjects<Bullet> _bullets;
    public event Action<Vector2> OnShoot;

    #region CoolDawn

    private int _shotsFired = 0;
    private float _cooldownTimer = 0f;

    #endregion

    public Player(Vector2 position, FabricController fabricController,  IConsoleInput input) : base(position, fabricController)
    {
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
    }

    public void Shoot()
    {
        
        _fabricController.BulletsFabric.CreateBullet(Position + CurrentDirection + Vector2.One, CurrentDirection);
        OnShoot?.Invoke(CurrentDirection);
        //Bullet bullet = new Bullet(Position + CurrentDirection + Vector2.One, CurrentDirection);
        //BulletObjects.Instance.AddObject(bullet);
    }

    public override void Destroy()
    {
        
    }
    
}