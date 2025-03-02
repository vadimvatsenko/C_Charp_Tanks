using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.Engine;
using C_Charp_Tanks.Engine.Ray;
using C_Charp_Tanks.Fabrics;
using C_Charp_Tanks.Systems;

namespace C_Charp_Tanks.Venicals.Enemy;

public class Enemy : Unit, IShoot
{
    private Unit _player;
    private Vector2 _target;
    
    private double _timeToMove = 0;
    private double _moveCooldown = 0.5f;
    private bool _isActive = false;
    
    private Vector2 _shootDirection;

    private int[] _dx = { -1, 0, 1, 0 };
    private int[] _dy = { 0, 1, 0, -1 };

    private double _timer = 1;

    // Можно ли стрелять
    private bool _canShoot = true;
    private double _currentShootTimer = 0;
    private const double CoolDownTime = 2.5;
    private bool _isShooting = false;
    
    private RayCast _rayCast;

    public Enemy(Vector2 position, FabricController fabricController, CollisionSystem collisionSystem) 
        : base(position, fabricController, collisionSystem)
    {
        UnitType = UnitType.Enemy;
        Speed = 2f;
        
        _player =
            _fabricController.UnitFabric.GetItems().Where(u => u.UnitType == UnitType.Player).FirstOrDefault();
    }

    public override void GetDamage(int damage)
    {
        base.GetDamage(damage);
    }

    public override void Update(double deltaTime)
    {
        ShootCoolDown(deltaTime);

        _rayCast = new RayCast(this.Position, CurrentDirection, 30);
        
        bool isPlayerDetection = _rayCast.CheckDetection(_player.Collider);
        
        if (isPlayerDetection && _canShoot) Shoot();
        
        if(_isShooting) return;

        List<Node> path = FindPath();

        if (path == null || path.Count <= 1) return;

        _timer -= (float)deltaTime;
        _isActive = _timer <= 0;

        Movement(path, deltaTime);
    }
    
    private void Movement(List<Node> path, double deltaTime)
    {
        if (_isActive)
        {
            Node nextPos = path[1];

            if (nextPos.Position.X > Position.X) CurrentDirection = Vector2.Right; // вправо
            if (nextPos.Position.X < Position.X) CurrentDirection = Vector2.Left; // влево
            if (nextPos.Position.Y > Position.Y) CurrentDirection = Vector2.Down; // вниз
            if (nextPos.Position.Y < Position.Y) CurrentDirection = Vector2.Up; // вверх

            SetDirection(CurrentDirection);

            _moveCooldown += deltaTime;
            // Рассчитываем, сколько шагов можно сделать за накопленное время
            while (_moveCooldown >= 1.0 / Speed)
            {
                _moveCooldown -= 1.0 / Speed; // Уменьшаем накопленное время
                Position = nextPos.Position;
                ; // Делаем шаг в направлении
            }
        }
    }

    private void SetDirection(Vector2 direction)
    {
        if (direction == Vector2.Up)
            View = PlayerData.Instance.TankUpView;
        else if (direction == Vector2.Down)
            View = PlayerData.Instance.TankDownView;
        else if (direction == Vector2.Left)
            View = PlayerData.Instance.TankLeftView;
        else if (direction == Vector2.Right)
            View = PlayerData.Instance.TankRightView;
    }
    
    public void Shoot()
    {
        if (!_canShoot) return;
        
        _isShooting = true;

        Vector2 bulletPos = Position + CurrentDirection + Vector2.One;
        Vector2 buttetDir = CurrentDirection;

        _fabricController.BulletsFabric.CreateBullet(bulletPos, buttetDir);
        _canShoot = false;
        _currentShootTimer = 0;
        
        Task.Delay(2000).ContinueWith(_ => _isShooting = false); // Задержка 2000 мс
    }
    
    private void ShootCoolDown(double deltaTime)
    {
        if (!_canShoot)
        {
            _currentShootTimer += deltaTime;
            if (_currentShootTimer >= CoolDownTime)
            {
                _canShoot = true;
            }
        }
    }
    
    private List<Node> FindPath()
    {
        Node startNode = new Node(Position);
        Node targetNode = new Node(_player.Position);

        List<Node> openList = new List<Node>() { startNode };
        List<Node> closedList = new List<Node>();

        while (openList.Count > 0)
        {
            Node currentNode = openList[0];

            foreach (var node in openList)
            {
                if (node.Value < targetNode.Value)
                {
                    currentNode = node;
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode.Position.Equals(targetNode.Position))
            {
                List<Node> path = new List<Node>();

                while (currentNode != null)
                {
                    path.Add(currentNode);
                    currentNode = currentNode.Parent;
                }

                path.Reverse();
                return path;
            }

            for (int i = 0; i < _dx.Length; i++)
            {
                int newX = currentNode.Position.X + _dx[i];
                int newY = currentNode.Position.Y + _dy[i];
                
                if (!_collisionSystem.IsUnwalkable(newX, newY))
                {
                    Node neighbor = new Node(new Vector2(newX, newY));

                    if (closedList.Contains(neighbor)) continue;

                    if (!openList.Contains(neighbor))
                    {
                        neighbor.Parent = currentNode;
                        neighbor.CalculateEstimate(_player.Position);
                        neighbor.CalculateValue();
                        openList.Add(neighbor);
                    }
                }
            }
        }

        return new List<Node>();
    }
    
}