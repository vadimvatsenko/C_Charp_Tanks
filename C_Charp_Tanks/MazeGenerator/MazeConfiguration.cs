namespace C_Charp_Tanks.MazeGenerator;

public class MazeConfiguration
{
    public int Width { get; }
    public int Height { get; }
    public float GapsChance { get; }

    public MazeConfiguration(int width, int height, float gapsChance)
    {
        Width = height; // тут инверсия будет
        Height = width;
        GapsChance = gapsChance;
    }
}