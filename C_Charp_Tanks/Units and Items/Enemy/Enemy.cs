using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.Engine;
using C_Charp_Tanks.Fabrics;

namespace C_Charp_Tanks.Venicals.Enemy;

public class Enemy : Unit
{
    private Vector2 _target;
    private double _timeToMove = 0;
    private double _moveCooldown = 0.5f;
    private bool _isActive = false;

    private Direction _direction = Direction.Up;
    
    private int[] _dx = { -1, 0, 1, 0 };
    private int[] _dy = { 0, 1, 0, -1};

    private double _timer = 1;
    
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
        _target = 
            _fabricController.UnitFabric.GetUnits().Where(u => u.UnitType == UnitType.Player).First().Position;
        List<Node> path = FindPath();
         
        if (path == null || path.Count <= 1) return;
            
        _timer -= (float)deltaTime;
        _isActive = _timer <= 0;
        
        if (_isActive)
        {
            Node nextPos = path[1];
            
            if (nextPos.Position.X > Position.X) _direction = Direction.Right; // вправо
            if (nextPos.Position.X < Position.X) _direction = Direction.Left; // влево
            if (nextPos.Position.Y > Position.Y) _direction = Direction.Down; // вниз
            if (nextPos.Position.Y < Position.Y) _direction = Direction.Up; // вверх
            
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
    
    private List<Node> FindPath()
    {
        Node startNode = new Node(Position);
        Node targetNode = new Node(_target);
        
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
                        neighbor.CalculateEstimate(_target);
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
            _fabricController.BlocksFabric.GetBlocks().Any(b => b.Collider.IsColliding(tempColl));
    }

    private void SetDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                View = PlayerData.Instance.TankUpView;
                break;
            case Direction.Down:
                View = PlayerData.Instance.TankDownView;
                break;
            case Direction.Left:
                View = PlayerData.Instance.TankLeftView;
                break;
            case Direction.Right:
                View = PlayerData.Instance.TankRightView;
                break;
        }
    }
}