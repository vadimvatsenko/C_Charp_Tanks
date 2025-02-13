using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.Fabrics;

namespace C_Charp_Tanks.Venicals.Enemy;

public class Enemy : Unit
{
    private Vector2 _target;
    private Direction _direction;
    private double _timeToMove = 0;
    private double _moveCooldown = 0.5f;
    private bool _isActive = false;
    private int[] _dx = { -1, 0, 1, 0 };
    private int[] _dy = { 0, 1, 0, -1};

    private double _timer = 1;
    
    public Enemy(Vector2 position, FabricController fabricController) : base(position, fabricController)
    {
        //_target = target;
    }

    public override void Update(double deltaTime)
    {
        _timer -= (float)deltaTime;
        _isActive = _timer <= 0;
        //Console.WriteLine(_timer);
        if (_isActive)
        {
            //_timeToMove -= deltaTime;
        
            //if(_timeToMove > 0) return;
        
            List<Node> path = FindPath();

            if (path == null || path.Count <= 1) return;
        
            Node nextPos = path[1];
            
            this.Position = nextPos.Position;
        
            //_timeToMove = _moveCooldown;

            /*if (Position == _target)
            {
                _target = new Vector2(50, 16);
            }*/
        
            //if(_target == null || path == null || path.Count <= 1) return;
        
            //Node nextPos = path[1];
        
            //TryToMove()
            //this.Position = nextPos.Position;
            //Console.WriteLine(this.Position.X);
        }
        
    }
    
    public void SetDirection(Direction direction) => _direction = direction;

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

        return new List<Node>();
    }
}