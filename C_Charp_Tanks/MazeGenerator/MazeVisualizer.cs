using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.Renderer;

namespace C_Charp_Tanks.MazeGenerator;

public class MazeVisualizer
{
    private Random _random = new Random();
    private BlocksController _blocksController;
    public List<Vector2> EmptyFields { get; private set; }
    
    private int Step => 3;
    
    public void Visualise(bool[,] maze)
    {
        DestroyOldMeshes();
        
        BuildWalls(maze);
        BuildWallsAround(maze);
    }

    public MazeVisualizer(BlocksController blocksController)
    {
        _blocksController = blocksController;
        EmptyFields = new List<Vector2>();
    }
    
    private void BuildWalls(bool[,] maze)
    {
       
        for (int i = 0; i < maze.GetLength(0); i += Step)
        for (int j = 0; j < maze.GetLength(1); j += Step)
        {
            if (!maze[i, j])
            {
                int randomBlock = _random.Next(2);
                if (randomBlock == 0)
                {
                    WaterBlock waterBlock = new WaterBlock(BlockType.Water, Symbols.Wall, new Vector2(i, j));
                    _blocksController.AddBlock(waterBlock);
                }
                else
                {
                    DestructibleBlock destructibleBlock = new DestructibleBlock(BlockType.Destructible, Symbols.Wall, new Vector2(i, j)); 
                    _blocksController.AddBlock(destructibleBlock);
                }
                
            }

            if (maze[i, j])
            {
                EmptyFields.Add(new Vector2(i, j));
            }
        }
    }

    private void BuildWallsAround(bool[,] maze)
    {
       
        for (int i = 0; i <= maze.GetLength(1); i += Step) // левая стена
        {
            IndestructibleBlock indestructibleBlockLeft =
                new IndestructibleBlock(BlockType.Indestructible, Symbols.Wall, new Vector2(0, i));
            _blocksController.AddBlock(indestructibleBlockLeft);

            IndestructibleBlock indestructibleBlockRight =
                new IndestructibleBlock(BlockType.Indestructible, Symbols.Wall, new Vector2(maze.GetLength(0), i));
            _blocksController.AddBlock(indestructibleBlockRight);

        }
        for (int j = 0; j <= maze.GetLength(0); j += Step)
        {
            IndestructibleBlock indestructibleBlockLeft =
               new IndestructibleBlock(BlockType.Indestructible, Symbols.Wall, new Vector2(j, 0));
            _blocksController.AddBlock(indestructibleBlockLeft);

            IndestructibleBlock indestructibleBlockRight =
                new IndestructibleBlock(BlockType.Indestructible, Symbols.Wall, new Vector2(j, maze.GetLength(1)));
            _blocksController.AddBlock(indestructibleBlockRight);
        }
    }

    private void DestroyOldMeshes()
    {
        /*foreach (Transform child in transform)        
            Destroy(child.gameObject);*/
        //_renderer.Clear();
    }
}