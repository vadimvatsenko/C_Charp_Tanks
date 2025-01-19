using C_Charp_Tanks;
using C_Charp_Tanks.Renderer;
using C_Charp_Tanks.Venicals;

public class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        ConsoleRenderer renderer0 = new ConsoleRenderer(Pallete.Colors);
        ConsoleRenderer renderer1 = new ConsoleRenderer(Pallete.Colors);
        
        ConsoleInput consoleInput = new ConsoleInput();
        
        Player player = new Player(new Vector2(5, 5), renderer0, consoleInput);

        Update update = new Update();
        

    }
}

