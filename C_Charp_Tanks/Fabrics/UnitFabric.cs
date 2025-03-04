using C_Charp_Tanks;
using C_Charp_Tanks.Fabrics;
using C_Charp_Tanks.Fabrics.BlocksFactory;
using C_Charp_Tanks.Systems;
using C_Charp_Tanks.Venicals;
using C_Charp_Tanks.Venicals.Enemy;
using C_Sharp_Maze_Generator.Maze;

public class UnitFabric : AbstractFabric<Unit>
{
    private readonly ConsoleInput _consoleInput;
    private readonly CollisionSystem _collisionSystem;
    private FabricController _fabricController;
    
    private Random _rand = new Random();
    private List<Vector2> _emptyPositions = new List<Vector2>();
    private int _level;
    public override event Action? OnItemsUpdated;
    
    public UnitFabric(ConsoleInput consoleInput, CollisionSystem collisionSystem)
    {
        _consoleInput = consoleInput;
        _collisionSystem = collisionSystem;
    }
    
    public void SetFabricController(FabricController fabricController)
    {
        _fabricController = fabricController;
    }
    public void SetLevel(int level)
    {
        _level = level;
    }
    
    public void CreateUnits()
    {
        _emptyPositions = _fabricController.EmptyPositions;
        if(_emptyPositions.Count <= 0) return;
        Vector2 unitPos = _emptyPositions[_rand.Next(0, _emptyPositions.Count)];
        Player player = new Player(unitPos, _fabricController, _consoleInput, _collisionSystem);
        
        _emptyPositions.Remove(unitPos);
        AddItem(player);

        for (int i = 0; i < _level; i++)
        {
            Vector2 enemyPos = _emptyPositions[_rand.Next(0, _emptyPositions.Count)];
            Enemy enemy = new Enemy(enemyPos, _fabricController, _collisionSystem);

            _emptyPositions.Remove(enemyPos);
            AddItem(enemy);
        }
    }
    
    public override void AddItem(Unit item)
    {
        _list.Add(item);
        OnItemsUpdated?.Invoke();
    }

    public override void RemoveItem(Unit item)
    {
        _list.Remove(item);
        OnItemsUpdated?.Invoke();
    }
    public override void Clear() => _list.Clear();
    public override List<Unit> GetItems() => _list;
 
}