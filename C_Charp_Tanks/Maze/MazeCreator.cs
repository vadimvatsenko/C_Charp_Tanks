using C_Charp_Tanks.Fabrics;
using C_Sharp_Maze_Generator.Maze;

namespace C_Charp_Tanks.Maze;

public class MazeCreator
{
    private MazeConfiguration _mazeConfiguration;
    private IMazeAlgorithm _mazeAlgorithm;
    private MazeGenerator _mazeGenerator;
    private Map _map;
    private char[,] _layer;
    private FabricController _fabricController;
    public MazeVisualizer _mazeVisualizer { get; private set; }
    
    private const int MazeWidth = 20;
    private const int MazeHeight = 10;
    private const float GapsChance = 0.0f;

    public MazeCreator(char[,] layer)
    {
        _layer = layer;
    }

    public void SetFabricController(FabricController fabricController)
    {
        _fabricController = fabricController;
    }

    public void Initialize() 
    {
        _mazeConfiguration = new MazeConfiguration(MazeWidth, MazeHeight, GapsChance);
        _mazeAlgorithm = new PrimsMazeGenerator();
        _mazeVisualizer = new MazeVisualizer(_fabricController, _layer);
        _mazeGenerator = new MazeGenerator(_mazeAlgorithm);
        _map = new Map(_mazeConfiguration, _mazeVisualizer);
    }
}