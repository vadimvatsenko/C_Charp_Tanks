using C_Charp_Tanks.Blocks;

namespace C_Charp_Tanks.MazeGenerator;

public class MazeVisualizer : IUpdatable
{
    ConsoleRenderer _renderer;
    List<Block> _blocks;

    private List<char> _symbolsList = new List<char>()
    {
        Symbols.Wall,
        Symbols.Water,
    };
    
    private Random _random = new Random();
    
    public MazeVisualizer(ConsoleRenderer renderer, List<Block> blocks)
    {
        _renderer = renderer;
        _blocks = blocks;
    }
    public void Visualise(bool[,] maze)
    {
        DestroyOldMeshes();
        
        BuildWalls(maze);
        BuildWallsAround(maze);
    }
    
    private void BuildWalls(bool[,] maze)
    {
        Console.WriteLine(maze.Length);
        for (int i = 0; i < maze.GetLength(0); i++)
        for (int j = 0; j < maze.GetLength(1); j++)
        {
            if (maze[i, j] == false)
            {
                /*for (int x = 0; x < _blocks[0].View.GetLength(0); x++)
                {
                    for (int y = 0; y < _blocks[0].View.GetLength(1); y++)
                    {
                        _renderer.SetPixel(i, j, _blocks[0].View[x,y], 3);
                    }
                }*/
                /////
                //_renderer.SetPixel(j, i, _symbolsList[_random.Next(_symbolsList.Count)], 7);
            }
        }
        
        _renderer.Render();
        
        
    }

    private void BuildWallsAround(bool[,] maze)
    {
        for (int i = 0; i <= maze.GetLength(0); i+=3)
        {
            for (int x = 0; x < _blocks[0].View.GetLength(0); x++)
            {
                for (int y = 0; y < _blocks[0].View.GetLength(1); y++)
                {
                    _renderer.SetPixel(x, i + y, _blocks[0].View[x,y], 3);
                    _renderer.SetPixel(maze.GetLength(1) + x, i + y, _blocks[0].View[x,y], 3);
                }
            }
        }

        for (int j = 0; j <= maze.GetLength(1); j+=3)
        {
            for (int x = 0; x < _blocks[0].View.GetLength(0); x++)
            {
                for (int y = 0; y < _blocks[0].View.GetLength(1); y++)
                {
                    _renderer.SetPixel(j + x, y, _blocks[0].View[x,y], 3);
                }
            }
            //_renderer.SetPixel(j, 0, Symbols.Wall, 1);
            //_renderer.SetPixel(j,  maze.GetLength(0), Symbols.Wall, 1);
            //Instantiate(_wallPrefab, new Vector3(j, 0, -1), Quaternion.identity, transform);
            //Instantiate(_wallPrefab, new Vector3(j, 0, maze.GetLength(0)), Quaternion.identity, transform);
        }
        
        _renderer.Render();
    }

    private void DestroyOldMeshes()
    {
        /*foreach (Transform child in transform)        
            Destroy(child.gameObject);*/
        _renderer.Clear();
    }

    public void Update(double deltaTime)
    {
        //_renderer.Render();
    }
}