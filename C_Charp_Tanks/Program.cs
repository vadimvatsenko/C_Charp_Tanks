using C_Charp_Tanks;
using C_Charp_Tanks.Renderer;
using C_Charp_Tanks.Venicals;

public class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Update update = new Update();
        IRenderer renderer = new ConsoleRenderer(Pallete.Colors);
        
        ConsoleInput input = new ConsoleInput();
        
        Player player = new Player(new Vector2(5,5), renderer, input);
        
        update.RegisterListener(input);
        update.RegisterListener(player);
        
        update.Start();
        
        

        /*ConsoleRenderer renderer = new ConsoleRenderer(Pallete.colors);


        Update update = new Update();
        Tank tank = new Tank();

        PlayerInput input = new PlayerInput(tank);

        GameRenderer gameRenderer = new GameRenderer(renderer, tank, GameData.Instance.Level);

        input.RegisterListener(tank);
        update.RegisterListener(tank);
        update.RegisterListener(input);
        update.RegisterListener(gameRenderer);


        update.Start();*/

        /*ConsoleRenderer renderer = new ConsoleRenderer(Pallete.colors);

        MazeGenerator mazeGenerator = new MazeGenerator(new PrimsMazeGenerator());
        MazeVisualizer mazeVisualizer = new MazeVisualizer(renderer);
        MazeConfiguration mazeConfiguration = new MazeConfiguration(28, 14, 0.5f);
        Map map = new Map(mazeGenerator, mazeVisualizer, mazeConfiguration);
        map.Init();
        */

    }
}

