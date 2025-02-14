using C_Charp_Tanks;
using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.Fabrics;
using C_Charp_Tanks.Fabrics.BlocksFactory;
using C_Charp_Tanks.Renderer;

namespace C_Sharp_Maze_Generator.Maze;

public class MazeVisualizer
{
    private Random _random = new Random();
    public List<Vector2> EmptyFields { get; private set; }
    public FabricController _fabricController;
    private int _step = 3;
    public void Visualise(bool[,] maze)
    {
        BuildWalls(maze);
        BuildWallsAround(maze);
    }

    public MazeVisualizer(FabricController fabricController) 
    {
        _fabricController = fabricController;
        EmptyFields = new List<Vector2>();
    }
    
    private void BuildWalls(bool[,] maze)
    {
        int mazeHeight = maze.GetLength(1);
        int mazeWidth = maze.GetLength(0);

        // Определяем начальную позицию отрисовки (по центру окна)
        int startX = Console.WindowWidth / 2 - mazeWidth / 2 * _step;
        int startY = Console.WindowHeight / 2 - mazeHeight / 2 * _step;
        
        for (int i = 0; i < mazeWidth - 1; i ++)
        for (int j = 0; j < mazeHeight - 1; j ++)
        {
            if (maze[i, j] == false)
            {
                int randomBlock = _random.Next(2);
                if (randomBlock == 0)
                {
                    WaterBlock waterBlock 
                        = new WaterBlock(
                            BlockType.Water, Symbols.Wall, new Vector2(i * _step + _step + startX, j * _step + _step));
                    _fabricController.BlocksFabric.AddBlock(waterBlock);
                }
                else
                {
                    
                    DestructibleBlock destructibleBlock 
                        = new DestructibleBlock(
                            _fabricController, BlockType.Destructible, Symbols.Wall, 
                            new Vector2(i * _step + _step + startX, j * _step + _step)); 
                    _fabricController.BlocksFabric.AddBlock(destructibleBlock);
                }
            }

            else
            {
                EmptyFields.Add(new Vector2(i * _step + _step + startX, j * _step + _step));
            }
        }
    }

    private void BuildWallsAround(bool[,] maze)
    {
        int mazeHeight = maze.GetLength(1);
        int mazeWidth = maze.GetLength(0);

        // Определяем начальную позицию отрисовки (по центру окна)
        int startX = Console.WindowWidth / 2 - mazeWidth / 2 * _step;
        int startY = Console.WindowHeight / 2 - mazeHeight / 2 * _step;
        
       // вертикаль
        for (int i = 0; i <= mazeHeight * _step; i += _step)
        {
            IndestructibleBlock indestructibleBlockLeft =
                new IndestructibleBlock(BlockType.Indestructible, Symbols.Wall, new Vector2(startX, i));
            _fabricController.BlocksFabric.AddBlock(indestructibleBlockLeft);

            IndestructibleBlock indestructibleBlockRight =
                new IndestructibleBlock(
                    BlockType.Indestructible, Symbols.Wall, new Vector2(startX + mazeWidth * _step, i));
            _fabricController.BlocksFabric.AddBlock(indestructibleBlockRight);

        }
        
        // горизонталь
        
        for (int j = 0; j <= mazeWidth * _step; j += _step)
        {
            IndestructibleBlock indestructibleBlockLeft =
               new IndestructibleBlock(
                   BlockType.Indestructible, Symbols.Wall, new Vector2(startX + j, 0));
            _fabricController.BlocksFabric.AddBlock(indestructibleBlockLeft);

            IndestructibleBlock indestructibleBlockRight =
                new IndestructibleBlock(BlockType.Indestructible, Symbols.Wall, new Vector2(startX + j, mazeHeight * _step));
            _fabricController.BlocksFabric.AddBlock(indestructibleBlockRight);
        }
    }
}