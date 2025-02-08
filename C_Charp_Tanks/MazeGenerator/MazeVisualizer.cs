using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.Engine.Window;
using C_Charp_Tanks.Renderer;
using C_Charp_Tanks.Venicals;

namespace C_Charp_Tanks.MazeGenerator;

public class MazeVisualizer
{
    private Random _random = new Random();
    private GameObjects<Block> _blockObjects;
    private WindowSettings _windowSettings;
    public List<Vector2> EmptyFields { get; private set; }

    
    
    private int Step => 3;
    
    public void Visualise(bool[,] maze)
    {
        DestroyOldMeshes();
        
        BuildWalls(maze);
        BuildWallsAround(maze);
    }

    public MazeVisualizer()
    {
        EmptyFields = new List<Vector2>();
    }
    
    private void BuildWalls(bool[,] maze)
    {
        int mazeHeight = maze.GetLength(1);
        int mazeWidth = maze.GetLength(0);

        // Определяем начальную позицию отрисовки (по центру окна)
        int startX = Console.WindowWidth / 2 - mazeWidth / 2;
        int startY = Console.WindowHeight / 2 - mazeHeight / 2;
        
        for (int i = 0; i < maze.GetLength(0); i += Step)
        for (int j = 0; j < maze.GetLength(1); j += Step)
        {
            if (!maze[i, j])
            {
                int randomBlock = _random.Next(2);
                if (randomBlock == 0)
                {
                    WaterBlock waterBlock = new WaterBlock(BlockType.Water, Symbols.Wall, new Vector2(startX + i, j));
                    BlockObjects.Instance.AddObject(waterBlock);
                }
                else
                {
                    DestructibleBlock destructibleBlock = new DestructibleBlock(BlockType.Destructible, Symbols.Wall, new Vector2(startX + i, j)); 
                    BlockObjects.Instance.AddObject(destructibleBlock);
                }
                
            }

            if (maze[i, j])
            {
                EmptyFields.Add(new Vector2(startX + i, startY + j));
            }
        }
        
        
    }

    private void BuildWallsAround(bool[,] maze)
    {
        int mazeHeight = maze.GetLength(1);
        int mazeWidth = maze.GetLength(0);

        // Определяем начальную позицию отрисовки (по центру окна)
        int startX = Console.WindowWidth / 2 - mazeWidth / 2;
        int startY = Console.WindowHeight / 2 - mazeHeight / 2;
       // вертикаль
        for (int i = 0; i <= mazeHeight; i += Step)
        {
            IndestructibleBlock indestructibleBlockLeft =
                new IndestructibleBlock(BlockType.Indestructible, Symbols.Wall, new Vector2(startX, i));
            BlockObjects.Instance.AddObject(indestructibleBlockLeft);

            IndestructibleBlock indestructibleBlockRight =
                new IndestructibleBlock(BlockType.Indestructible, Symbols.Wall, new Vector2(startX + mazeWidth, i));
            BlockObjects.Instance.AddObject(indestructibleBlockRight);

        }
        
        // горизонталь
        
        for (int j = 0; j <= mazeWidth; j += Step)
        {
            IndestructibleBlock indestructibleBlockLeft =
               new IndestructibleBlock(BlockType.Indestructible, Symbols.Wall, new Vector2(startX + j, 0));
            BlockObjects.Instance.AddObject(indestructibleBlockLeft);

            IndestructibleBlock indestructibleBlockRight =
                new IndestructibleBlock(BlockType.Indestructible, Symbols.Wall, new Vector2(startX + j, mazeHeight));
            BlockObjects.Instance.AddObject(indestructibleBlockRight);
        }
    }

    private void DestroyOldMeshes()
    {
        /*foreach (Transform child in transform)        
            Destroy(child.gameObject);*/
        //_renderer.Clear();
    }
}