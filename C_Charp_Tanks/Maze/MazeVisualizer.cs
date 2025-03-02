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
    private readonly int _step = 3;
    
    public int StartX { get; private set; }
    public int StartY { get; private set; }
    public int EndX { get; private set; }
    public int MazeWidth { get; private set; }
    public int MazeHeight { get; private set; }
    
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
        MazeHeight = maze.GetLength(1);
        MazeWidth = maze.GetLength(0);

        // Определяем начальную позицию отрисовки (по центру окна)
        StartX = (Console.WindowWidth / 2 - MazeWidth / 2 * _step) + _step;
        StartY = (Console.WindowHeight / 2 - MazeHeight / 2 * _step) + _step;
        
        EndX = Console.WindowWidth / 2 + MazeWidth / 2 * _step;
        
        for (int i = 0; i < MazeWidth - 1; i ++)
        for (int j = 0; j < MazeHeight - 1; j ++)
        {
            if (maze[i, j] == false)
            {
                int randomBlock = _random.Next(2);
                if (randomBlock == 0)
                {
                    //Vector2 pos = new Vector2(i * _step + _step, j * _step + _step); // - лево верх
                    //Vector2 pos = new Vector2(i * _step + _step + StartX, j * _step + _step); // - центр верх
                    Vector2 pos = new Vector2(i * _step + _step + StartX, j * _step + _step + StartY); // - центр - центр
                    WaterBlock waterBlock = new WaterBlock(BlockType.Water, Symbols.WaterStateOne, pos);
                    _fabricController.BlocksFabric.AddItem(waterBlock);
                }
                else
                {
                    //Vector2 pos = new Vector2(i * _step + _step, j * _step + _step); // - лево верх
                    //Vector2 pos = new Vector2(i * _step + _step + StartX, j * _step + _step); // - центр верх
                    Vector2 pos = new Vector2(i * _step + _step + StartX, j * _step + _step + StartY); // - центр - центр
                    DestructibleBlock destructibleBlock =
                        new DestructibleBlock(BlockType.Destructible, Symbols.Wall, pos);
                    _fabricController.BlocksFabric.AddItem(destructibleBlock);
                }
            }

            else
            {
                //Vector2 pos = new Vector2(i * _step + _step, j * _step + _step); // - лево верх
                //Vector2 pos = new Vector2(i * _step + _step + StartX, j * _step + _step); // - центр верх
                Vector2 pos = new Vector2(i * _step + _step + StartX, j * _step + _step + StartY); // - центр - центр
               _fabricController.AddEmptyPosition(pos);
            }
        }
    }

    private void BuildWallsAround(bool[,] maze)
    {
       // вертикаль
       // левая
        for (int i = 0; i <= MazeHeight * _step; i += _step)
        {
            //Vector2 posY1 = new Vector2(0, i); // лево - верх
            //Vector2 posY1 = new Vector2(StartX, i); // центр - верх
            Vector2 posY1 = new Vector2(StartX, i + StartY); // центр - центр
            IndestructibleBlock indestructibleBlockLeft =
                new IndestructibleBlock(BlockType.Indestructible, Symbols.Wall, posY1);
            _fabricController.BlocksFabric.AddItem(indestructibleBlockLeft);
            
            // правая
            //Vector2 posY2 = new Vector2(MazeWidth * _step, i); // лево - верх
            //Vector2 posY2 = new Vector2(StartX + MazeWidth * _step, i); // центр - верх
            Vector2 posY2 = new Vector2(StartX + MazeWidth * _step, i + StartY); // центр - центр
            IndestructibleBlock indestructibleBlockRight =
                new IndestructibleBlock(
                    BlockType.Indestructible, Symbols.Wall, posY2);
            _fabricController.BlocksFabric.AddItem(indestructibleBlockRight);

        }
        // горизонталь
        for (int j = 0; j <= MazeWidth * _step; j += _step)
        {
            //Vector2 posX1 = new Vector2(j, 0); // лево - вверх
            Vector2 posX1 = new Vector2(StartX + j, StartY); // центр - центр
            IndestructibleBlock indestructibleBlockLeft =
               new IndestructibleBlock(
                   BlockType.Indestructible, Symbols.Wall, posX1);
            _fabricController.BlocksFabric.AddItem(indestructibleBlockLeft);
            
            //Vector2 posX2 = new Vector2(j, MazeHeight * _step); // лево - верх
            Vector2 posX2 = new Vector2(j + StartX, MazeHeight * _step + StartY); // центр - центр
            IndestructibleBlock indestructibleBlockRight =
                new IndestructibleBlock(BlockType.Indestructible, Symbols.Wall, posX2);
            _fabricController.BlocksFabric.AddItem(indestructibleBlockRight);
        }
    }
}