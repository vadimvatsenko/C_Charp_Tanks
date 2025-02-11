using C_Charp_Tanks;
using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.MazeGenerator;
using C_Charp_Tanks.Venicals;
using C_Charp_Tanks.Venicals.Enemy;

public class UnitFabric
{
    private ConsoleInput _consoleInput;
    private MazeCreator _mazeCreator;
    private Random _rand = new Random();
    
    private List<Vector2> _emptyPositions = new List<Vector2>();
    public UnitFabric(ConsoleInput consoleInput, MazeCreator mazeCreator)
    {
        _consoleInput = consoleInput;
        _mazeCreator = mazeCreator;
        _emptyPositions = _mazeCreator._mazeVisualizer.EmptyFields;
    }

    public void CreateUnits(int level = 3)
    {
        Vector2 unitPos = _emptyPositions[_rand.Next(0, _emptyPositions.Count)];
        Player player = new Player(unitPos, _consoleInput);
        
        _emptyPositions.Remove(unitPos);
        UnitObjects.Instance.AddObject(player);

        for (int i = 0; i < level; i++)
        {
            Vector2 enemyPos = _emptyPositions[_rand.Next(0, _emptyPositions.Count)];
            Enemy enemy = new Enemy(enemyPos, player.Position);

            _emptyPositions.Remove(enemyPos);
            UnitObjects.Instance.AddObject(enemy);
        }
    }
}