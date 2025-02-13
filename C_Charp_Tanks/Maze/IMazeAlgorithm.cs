namespace C_Sharp_Maze_Generator.Maze;

public interface IMazeAlgorithm
{
    bool[,] Generate(int width, int height);
}