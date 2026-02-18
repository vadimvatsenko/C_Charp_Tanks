namespace C_Charp_Tanks.Engine;

public class MapConfig
{
    public int Width { get; private set; }
    public int Height { get; private set; }

    public MapConfig(int width, int height)
    {
        Width = width;
        Height = height;
    }
}