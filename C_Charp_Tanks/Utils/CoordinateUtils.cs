using C_Charp_Tanks;

namespace C_Sharp_Maze_Generator.Utils;

public class CoordinateUtils
{
    public int StartX { get; private set; }
    public int StartY { get; private set; }
    public int EndX { get; private set; }
    public int EndY { get; private set; }
    public int Width => EndX - StartX;
    public int Height => EndY - StartY;
    
    public int ScreenWidth = Console.WindowWidth;
    public int ScreenHeight = Console.WindowHeight;
    
    public int CenterX => StartX + (ScreenWidth - Width) / 2;
    public int CenterY => StartY + (ScreenHeight - Height) / 2;

    public static Vector2 Coordinate(int x, int y)
    {
        return new Vector2(x, y);
    }
}