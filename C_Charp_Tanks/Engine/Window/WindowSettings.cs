namespace C_Charp_Tanks.Engine.Window;

public class WindowSettings
{
    public int windowWidth { get; private set; }
    public int windowHeight { get; private set; }
    public WindowSettings(int width, int height)
    {
        Console.WindowWidth = width;
        Console.WindowHeight = height;
        
        windowWidth = width;
        windowHeight = height;
        
        Console.WriteLine($"Максимальная ширина: {Console.LargestWindowWidth}");
        Console.WriteLine($"Максимальная высота: {Console.LargestWindowHeight}");
    }
}