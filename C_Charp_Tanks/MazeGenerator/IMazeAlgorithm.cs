namespace C_Charp_Tanks.MazeGenerator;

public interface IMazeAlgorithm
{
    bool[,] Generate(int width, int height);
}