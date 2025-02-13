using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.Fabrics.BlocksFactory;
using C_Charp_Tanks.Renderer;

namespace C_Charp_Tanks.MazeGenerator;

public class MazeVisualizer
{
    private Random _random = new Random();
    public List<Vector2> EmptyFields { get; private set; }
    public BlocksFabric _blocksFabric { get; private set; }
    
    
    private IRenderer _renderer; //

    private int _step = 3;
    
    public void Visualise(bool[,] maze)
    {
        BuildWalls(maze);
        BuildWallsAround(maze);
    }

    public MazeVisualizer(BlocksFabric blocksFabric, IRenderer renderer) 
    {
        _renderer = renderer; //
        _blocksFabric = blocksFabric;
        EmptyFields = new List<Vector2>();
    }
    
    /*private void BuildWalls(bool[,] maze)
    {
        int mazeHeight = maze.GetLength(1);
        int mazeWidth = maze.GetLength(0);

        // Определяем начальную позицию отрисовки (по центру окна)
        int startX = Console.WindowWidth / 2 - mazeWidth / 2;
        int startY = Console.WindowHeight / 2 - mazeHeight / 2;
        
        for (int i = 0; i < mazeWidth; i ++)
        for (int j = 0; j < mazeHeight; j ++)
        {
            if (maze[i, j] == false)
            {
                _renderer.SetPixel(i, j, '8', 5);
                int randomBlock = _random.Next(2);
                if (randomBlock == 0)
                {
                    WaterBlock waterBlock = new WaterBlock(BlockType.Water, Symbols.Wall, new Vector2(i * _step + startX , j * _step + _step));
                    _blocksFabric.AddBlock(waterBlock);
                }
                else
                {
                    DestructibleBlock destructibleBlock = new DestructibleBlock(BlockType.Destructible, Symbols.Wall, new Vector2(i * _step + startX , j * _step + _step)); 
                    _blocksFabric.AddBlock(destructibleBlock);
                }
            }

            else
            {
                //EmptyFields.Add(new Vector2(startX + i, j));
            }
            
        }
    }*/

    /*private void BuildWallsAround(bool[,] maze)
    {
        int mazeHeight = maze.GetLength(1);
        int mazeWidth = maze.GetLength(0);

        // Определяем начальную позицию отрисовки (по центру окна)
        int startX = Console.WindowWidth / 2 - mazeWidth / 2 * _step;
        int startY = Console.WindowHeight / 2 - mazeHeight / 2 * _step;
        
       // вертикаль
        for (int i = 0; i < mazeHeight * _step; i += _step)
        {
            IndestructibleBlock indestructibleBlockLeft =
                new IndestructibleBlock(BlockType.Indestructible, Symbols.Wall, new Vector2(startX - _step, i));
            _blocksFabric.AddBlock(indestructibleBlockLeft);

            IndestructibleBlock indestructibleBlockRight =
                new IndestructibleBlock(BlockType.Indestructible, Symbols.Wall, new Vector2((startX + _step) + mazeWidth * _step, i));
            _blocksFabric.AddBlock(indestructibleBlockRight);

        }
        
        // горизонталь
        
        for (int j = 0; j <= mazeWidth * _step; j += _step)
        {
            IndestructibleBlock indestructibleBlockLeft =
               new IndestructibleBlock(BlockType.Indestructible, Symbols.Wall, new Vector2(startX + j, 0));
            _blocksFabric.AddBlock(indestructibleBlockLeft);

            IndestructibleBlock indestructibleBlockRight =
                new IndestructibleBlock(BlockType.Indestructible, Symbols.Wall, new Vector2(startX + j, mazeHeight * _step - _step));
            _blocksFabric.AddBlock(indestructibleBlockRight);
        }
    }*/
    
    private void BuildWalls(bool[,] maze)
    {
        for (int i = 0; i < maze.GetLength(0); i++)
        for (int j = 0; j < maze.GetLength(1); j++)
            if (maze[i, j] == false)
            {
                //Block block = new Block(new Vector2(i, j), '#');
                _renderer.SetPixel(j, i, 'X', 2);
            }
        //Instantiate(_wallPrefab, new Vector3(j, 0, i), Quaternion.identity, transform);
        //_renderer.Render();
    }

    private void BuildWallsAround(bool[,] maze)
    {
        for (int i = 0; i <= maze.GetLength(0); i++)
        {
            //Instantiate(_wallPrefab, new Vector3(-1, 0, i), Quaternion.identity, transform);
            //Instantiate(_wallPrefab, new Vector3(maze.GetLength(1), 0, i), Quaternion.identity, transform);
            /*_renderer.SetPixel(0, i, '#', 3);
            _renderer.SetPixel(maze.GetLength(1), i, '#', 3);*/
        }

        for (int j = 0; j <= maze.GetLength(1); j++)
        {
            //Instantiate(_wallPrefab, new Vector3(j, 0, -1), Quaternion.identity, transform);
            //Instantiate(_wallPrefab, new Vector3(j, 0, maze.GetLength(0)), Quaternion.identity, transform);
        }
    }
}