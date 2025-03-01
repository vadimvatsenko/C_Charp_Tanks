namespace C_Sharp_Maze_Generator.Maze;

public class Map
{
    private Random _random = new();
    public Action onMapChanged;
    
    private MazeGenerator _mazeGenerator;
    private MazeConfiguration _mazeConfiguration;
    private MazeVisualizer _mazeVisualizer;
    public bool[,] map { get; private set; }  // false - wall, true - road
    
    
    public Map(MazeConfiguration mazeConfiguration, MazeVisualizer mazeVisualizer)
    {
        
        _mazeGenerator = new MazeGenerator(new PrimsMazeGenerator());
        _mazeConfiguration = mazeConfiguration;
        _mazeVisualizer = mazeVisualizer;
        
        GenerateMaze(
            _mazeConfiguration.Width, 
            _mazeConfiguration.Height, 
            _mazeConfiguration.GapChance);
    }
    
    public void GenerateMaze(int width, int height, float gaps)
    {
        map = _mazeGenerator.Generate(width, height, gaps);
        _mazeVisualizer.Visualise(map);

        PlaceHero();

        onMapChanged?.Invoke();
    }
    
    private void PlaceHero()
    {
        int x = _random.Next(0, map.GetLength(1));
        int y = _random.Next(0, map.GetLength(0));

        var closestRoad = MazeHelper.GetClosestRoad(map, x, y);
        
        //_hero.position = new Vector3(closestRoad.Item1, 0, closestRoad.Item2);
    }
}

