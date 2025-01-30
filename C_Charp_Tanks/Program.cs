using C_Charp_Tanks;
using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.MazeGenerator;
using C_Charp_Tanks.Renderer;
using C_Charp_Tanks.Venicals;

public class Program
{
    public static void Main(string[] args)
    {
        // нужно в mapGenerator создавать блоки, а сами блоки обновлять непосредственно в Update
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        
        ConsoleRenderer renderer0 = new ConsoleRenderer(Pallete.Colors);
        /*ConsoleRenderer renderer1 = new ConsoleRenderer(Pallete.Colors);
        
        ConsoleInput consoleInput = new ConsoleInput();
        
        Player player = new Player(new Vector2(5, 5), renderer0, consoleInput);
        

        Update update = new Update();

        update.AddUpdateListener(player);
        update.AddUpdateListener(consoleInput);*/
        
        //ConsoleRenderer consoleRenderer = new ConsoleRenderer(Pallete.Colors);

        List<Block> blocks = new List<Block>()
        {
            new IndestructibleBlock(BlockType.Indestructible, Symbols.Wall),
        };

        MazeConfiguration mazeConfiguration = new MazeConfiguration(60, 60, 0.1f);
        MazeVisualizer mazeVisualizer = new MazeVisualizer(renderer0, blocks); // тут ничего не передаем
        PrimsMazeGenerator primsMazeGenerator = new PrimsMazeGenerator();
        MazeGenerator mazeGenerator = new MazeGenerator(primsMazeGenerator);
        
        
        Map map = new Map(mazeGenerator, mazeVisualizer, mazeConfiguration);
        
        map.Init();
        
        //update.AddUpdateListener(mazeVisualizer);
        
        //update.StartUpdate();

    }
}

