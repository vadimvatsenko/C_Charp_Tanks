namespace C_Sharp_Maze_Generator.Maze;

public struct MazeConfiguration
{
    public int Width { get; }
    public int Height { get; }
    public float GapChance { get;}
    public MazeConfiguration(int width, int height, float gapsChance)
    {
        Width = height; // инверсия
        Height = width;
        GapChance = gapsChance;
    }
    
}