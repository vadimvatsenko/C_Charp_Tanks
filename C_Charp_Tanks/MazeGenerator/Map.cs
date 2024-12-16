namespace C_Charp_Tanks.MazeGenerator;

public class Map
{
    public event Action onMapChanged;
    
    private MazeGenerator _mazeGenerator;
    private MazeVisualizer _mazeVisualizer;
    private MazeConfiguration _mazeConfiguration;
    public bool[,] map { get; private set; }  // false - wall, true - road

    public Map(MazeGenerator mazeGenerator, MazeVisualizer mazeVisualizer, MazeConfiguration mazeConfiguration)
    {
        _mazeGenerator = mazeGenerator;
        _mazeVisualizer = mazeVisualizer;
        _mazeConfiguration = mazeConfiguration;
        
    }

    public void GenerateMaze(int width, int height, float gaps)
    {
        map = _mazeGenerator.Generate(width, height, gaps);
        _mazeVisualizer.Visualise(map);
        
        onMapChanged?.Invoke();
    }
    
    public void Init()
    {
        GenerateMaze(
            _mazeConfiguration.Width, 
            _mazeConfiguration.Height, 
            _mazeConfiguration.GapsChance);
    }
    
}