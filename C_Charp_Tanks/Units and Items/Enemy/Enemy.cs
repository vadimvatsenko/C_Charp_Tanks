using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.Engine;
using C_Charp_Tanks.Engine.Ray;
using C_Charp_Tanks.Fabrics;

namespace C_Charp_Tanks.Venicals.Enemy;

public class Enemy : Unit, IShoot
{
    private Unit _player;
    private double _timeToMove = 0;
    private double _moveCooldown = 0.5f;
    private bool _isActive = false;
    private bool _isPlayerInZone = false;

    private Vector2 _direction = Vector2.Up;
    private Vector2 _shootDirection;
    
    private int[] _dx = { -1, 0, 1, 0 };
    private int[] _dy = { 0, 1, 0, -1};

    private double _timer = 1;
    
    private float _shootCooldown = 2f;
    private float _currentShootTimer = 0f;
    
    public Enemy(Vector2 position, FabricController fabricController) : base(position, fabricController)
    {
        UnitType = UnitType.Enemy;
        Speed = 2f;
    }

    public override void GetDamage(int damage)
    {
        base.GetDamage(damage);
    }

    public override void Update(double deltaTime)
    {
        FindPlayer();

        if (RayCast())
        {
            Shoot();
        }
        
        List<Node> path = FindPath();
         
        if (path == null || path.Count <= 1) return;
            
        _timer -= (float)deltaTime;
        _isActive = _timer <= 0;
        
        if (_isActive)
        {
            Node nextPos = path[1];
            
            if (nextPos.Position.X > Position.X) _direction = Vector2.Right; // вправо
            if (nextPos.Position.X < Position.X) _direction = Vector2.Left; // влево
            if (nextPos.Position.Y > Position.Y) _direction = Vector2.Down; // вниз
            if (nextPos.Position.Y < Position.Y) _direction = Vector2.Up; // вверх
            
            SetDirection(_direction);
            
            _moveCooldown += deltaTime;
            // Рассчитываем, сколько шагов можно сделать за накопленное время
            while (_moveCooldown >= 1.0 / Speed)
            {
                _moveCooldown -= 1.0 / Speed; // Уменьшаем накопленное время
                Position = nextPos.Position;; // Делаем шаг в направлении
            }
        }
    }

    private void FindPlayer()
    {
        _player = 
            _fabricController.UnitFabric.GetItem().Where(u => u.UnitType == UnitType.Player).FirstOrDefault();
    }

    private bool RayCast()
    {
        int maxDistance = 30;
    
        for (int i = 1; i <= maxDistance; i++) // i идёт от 1, чтобы не проверять свою же позицию
        {
            Vector2 tempPos = this.Position + _direction * i; // Смещаемся в направлении игрока
            if (new BoxCollider2D(tempPos, new Vector2(1, 1)).IsColliding(_player.Collider))
            {
                return true;
            }
        }
    
        return false;
    }

    private List<Node> FindPath()
    {
        Node startNode = new Node(Position);
        Node targetNode = new Node(_player.Position);
        
        List<Node> openList = new List<Node>() {startNode};
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
                
                // нужно проверить столкновение
                
                if (!IsUnwalkable(newX, newY))
                {
                    Node neighbor  = new Node(new Vector2(newX, newY));
                
                    if(closedList.Contains(neighbor)) continue;
                    
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
    
    private bool IsUnwalkable(int x, int y) 
    {
        BoxCollider2D tempColl = new BoxCollider2D(new Vector2(x,y), new Vector2(3, 3));

        return
            _fabricController.BlocksFabric.GetItem().Any(b => b.Collider.IsColliding(tempColl));
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
        if (_currentShootTimer > 0f)
        {
            return;
        }
        
        Vector2 shellPosition = Position + _shootDirection + Vector2.One;
        Vector2 shellDirection = CurrentDirection;
        
        _fabricController.ShellsFabric.CreateShell(
            _fabricController, 
            shellPosition, 
            shellDirection
        );
        
        _currentShootTimer = _shootCooldown;
    }

    public override void Destroy()
    {
        base.Destroy();
    }
}