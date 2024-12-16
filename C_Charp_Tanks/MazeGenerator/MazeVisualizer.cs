namespace C_Charp_Tanks.MazeGenerator;

public class MazeVisualizer
{
    ConsoleRenderer _renderer;

    public MazeVisualizer(ConsoleRenderer renderer)
    {
        _renderer = renderer;
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
                _renderer.SetPixel(j, i, '\u25a6', 7);
            }
        }
    }

    private void BuildWallsAround(bool[,] maze)
    {
        for (int i = 0; i <= maze.GetLength(0); i++)
        {
            //Instantiate(_wallPrefab, new Vector3(-1, 0, i), Quaternion.identity, transform);
            //Instantiate(_wallPrefab, new Vector3(maze.GetLength(1), 0, i), Quaternion.identity, transform);
            _renderer.SetPixel(0, i, '\u25a6', 1);
            _renderer.SetPixel(maze.GetLength(1),  i, '\u25a6', 1);
        }

        for (int j = 0; j <= maze.GetLength(1); j++)
        {
            _renderer.SetPixel(j, 0, '\u25a6', 1);
            _renderer.SetPixel(j,  maze.GetLength(0), '\u25a6', 1);
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
}