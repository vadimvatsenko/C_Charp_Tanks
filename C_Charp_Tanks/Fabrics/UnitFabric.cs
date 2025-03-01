using C_Charp_Tanks;
using C_Charp_Tanks.Fabrics;
using C_Charp_Tanks.Fabrics.BlocksFactory;
using C_Charp_Tanks.Venicals;
using C_Charp_Tanks.Venicals.Enemy;
using C_Sharp_Maze_Generator.Maze;

public class UnitFabric : AbstractFabric<Unit>
{
    private readonly ConsoleInput _consoleInput;
    private readonly MazeCreator _mazeCreator;
    private FabricController _fabricController;
    private Random _rand = new Random();
    
    private List<Vector2> _emptyPositions = new List<Vector2>();
    
    private int _level;
    
    public UnitFabric(ConsoleInput consoleInput)
    {
        _consoleInput = consoleInput;
        _list = new List<Unit>();
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
        Player player = new Player(unitPos, _fabricController, _consoleInput);
        
        _emptyPositions.Remove(unitPos);
        AddItem(player);

        for (int i = 0; i < _level; i++)
        {
            Vector2 enemyPos = _emptyPositions[_rand.Next(0, _emptyPositions.Count)];
            Enemy enemy = new Enemy(enemyPos, _fabricController);

            _emptyPositions.Remove(enemyPos);
            AddItem(enemy);
        }
    }
}