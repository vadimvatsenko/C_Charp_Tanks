namespace C_Charp_Tanks.MazeGenerator;

public class MazeConfiguration
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    public float GapsChance { get; private set; }

    public MazeConfiguration(int width, int height, float gapsChance)
    {
        Width = width;
        Height = height;
        GapsChance = gapsChance;
    }
}