using C_Charp_Tanks;
using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.MazeGenerator;
using C_Charp_Tanks.Renderer;
using C_Charp_Tanks.Venicals;

public class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        
        ConsoleRenderer renderer0 = new ConsoleRenderer(Pallete.Colors);
        /*ConsoleRenderer renderer1 = new ConsoleRenderer(Pallete.Colors);
        
        ConsoleInput consoleInput = new ConsoleInput();
        
        Player player = new Player(new Vector2(5, 5), renderer0, consoleInput);
        
        IndestructibleBlock block = new IndestructibleBlock(new Vector2(5, 5), renderer0, BlockType.Indestructible);

        Update update = new Update();

        update.AddUpdateListener(player);
        update.AddUpdateListener(consoleInput);*/
        //update.AddUpdateListener(block);
        
        

        //ConsoleRenderer consoleRenderer = new ConsoleRenderer(Pallete.Colors);
        

        MazeConfiguration mazeConfiguration = new MazeConfiguration(30, 30, 0.1f);
        MazeVisualizer mazeVisualizer = new MazeVisualizer(renderer0);
        PrimsMazeGenerator primsMazeGenerator = new PrimsMazeGenerator();
        MazeGenerator mazeGenerator = new MazeGenerator(primsMazeGenerator);
        
        
        Map map = new Map(mazeGenerator, mazeVisualizer, mazeConfiguration);
        
        map.Init();
        
        /*update.AddUpdateListener(mazeVisualizer);
        
        update.StartUpdate();*/

    }
}

