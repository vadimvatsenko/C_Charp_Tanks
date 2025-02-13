using C_Charp_Tanks.Fabrics.BlocksFactory;
using C_Charp_Tanks.Renderer;

namespace C_Sharp_Maze_Generator.Maze;

public class MazeCreator
{
    private MazeConfiguration _mazeConfiguration;
    private IMazeAlgorithm _mazeAlgorithm;
    private MazeGenerator _mazeGenerator;
    
    private BlocksFabric _blocksFabric;
    
    private IRenderer _renderer; //
    public MazeVisualizer _mazeVisualizer { get; private set; }
    private Map _map;

    private int _mazeWidth;
    private int _mazeHeight;

    private float _gapsChance = 0.0f;

    public MazeCreator(BlocksFabric blocksFabric)
    {
        _blocksFabric = blocksFabric;
        _mazeWidth = Console.WindowWidth <= 120 ? Console.WindowWidth - 2 : 120;
        _mazeHeight = Console.WindowHeight <= 60 ? Console.WindowHeight - 2 : 60;
    }

    public void Initialize() 
    {
        _mazeConfiguration = new MazeConfiguration(20, 10, _gapsChance);
        _mazeAlgorithm = new PrimsMazeGenerator();
        _mazeVisualizer = new MazeVisualizer(_blocksFabric); //
        _mazeGenerator = new MazeGenerator(_mazeAlgorithm);
        _map = new Map(_mazeConfiguration, _mazeVisualizer);
    }
}