using C_Charp_Tanks;
using C_Charp_Tanks.Fabrics;
using C_Charp_Tanks.Venicals;
using C_Charp_Tanks.Venicals.Enemy;
using C_Sharp_Maze_Generator.Maze;

public class UnitFabric : AbstractFabric<Unit>
{
    public override event Action<Unit>? OnItemDestroyed;
    public override event Action? OnItemCreated;
    
    private FabricController _fabricController;
    private ConsoleInput _consoleInput;
    private MazeCreator _mazeCreator;
    
    private List<Vector2> _emptyPositions = new List<Vector2>();
    private Random _rand = new Random();

    private int _level;
    
    public UnitFabric(ConsoleInput consoleInput, MazeCreator mazeCreator)
    {
        _consoleInput = consoleInput;
        _mazeCreator = mazeCreator;
        _list = new List<Unit>();

        OnItemCreated += CreateItem;
        OnItemDestroyed += RemoveItem;
    }

    ~UnitFabric()
    {
        OnItemCreated -= CreateItem;
        OnItemDestroyed -= RemoveItem;
    }

    public void SetFabricController(FabricController fabricController)
    {
        _fabricController = fabricController;
    }
    public void SetLevel(int level)
    {
        _level = level;
    }
    
    public override void CreateItem()
    {
        _emptyPositions = _mazeCreator._mazeVisualizer.EmptyFields;
        if(_emptyPositions.Count <= 0) return;
        Vector2 unitPos = _emptyPositions[_rand.Next(0, _emptyPositions.Count)];
        Player player = new Player(unitPos, _fabricController, _consoleInput);
        
        _emptyPositions.Remove(unitPos);
        _list.Add(player);

        for (int i = 0; i < _level; i++)
        {
            Vector2 enemyPos = _emptyPositions[_rand.Next(0, _emptyPositions.Count)];
            Enemy enemy = new Enemy(enemyPos, _fabricController);

            _emptyPositions.Remove(enemyPos);
            _list.Add(enemy);
        }
    }

    public override void AddItem(Unit item) => _list.Add(item);
    public override void RemoveItem(Unit item) => _list.Remove(item);
    public override List<Unit> GetItem() => _list;
    public override void ClearItem() => _list.Clear();
}