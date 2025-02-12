using C_Charp_Tanks;
using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.Fabrics;
using C_Charp_Tanks.MazeGenerator;
using C_Charp_Tanks.Venicals;
using C_Charp_Tanks.Venicals.Enemy;

public class UnitFabric : AbstractUnitsFactory
{
    private ConsoleInput _consoleInput;
    private MazeCreator _mazeCreator;
    private Random _rand = new Random();
    private List<Vector2> _emptyPositions = new List<Vector2>();
    
    public UnitFabric(ConsoleInput consoleInput, MazeCreator mazeCreator = null)
    {
        _units = new List<Unit>();
        _consoleInput = consoleInput;
        _mazeCreator = mazeCreator;
        _emptyPositions = mazeCreator._mazeVisualizer.EmptyFields;
    }
    
    public override void AddUnit(Unit unit) => _units.Add(unit);
    public override void RemoveUnit(Unit unit) => _units.Remove(unit);
    public override List<Unit> GetUnits() => _units;
    
    public override void CreateUnits(int level = 3)
    {
        if(_emptyPositions.Count <= 0) return;
        Vector2 unitPos = _emptyPositions[_rand.Next(0, _emptyPositions.Count)];
        Player player = new Player(unitPos, _consoleInput);
        
        _emptyPositions.Remove(unitPos);
        _units.Add(player);

        for (int i = 0; i < level; i++)
        {
            Vector2 enemyPos = _emptyPositions[_rand.Next(0, _emptyPositions.Count)];
            Enemy enemy = new Enemy(enemyPos, player.Position);

            _emptyPositions.Remove(enemyPos);
            _units.Add(enemy);
        }
    }

    
    
}