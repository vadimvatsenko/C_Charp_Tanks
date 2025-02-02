using C_Charp_Tanks;
using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.MazeGenerator;
using C_Charp_Tanks.Renderer;
using C_Charp_Tanks.Venicals;

public class Program
{
    const float targetFrameTime = 1f / 60f;
    public static void Main(string[] args)
    {
        
        // нужно в mapGenerator создавать блоки, а сами блоки обновлять непосредственно в Update
        /*Console.OutputEncoding = System.Text.Encoding.UTF8;
        
        ConsoleRenderer renderer0 = new ConsoleRenderer(Pallete.Colors);
        ConsoleRenderer renderer1 = new ConsoleRenderer(Pallete.Colors);
        
        ConsoleRenderer prevRenderer = renderer0;
        ConsoleRenderer currentRenderer = renderer1;
        
        ConsoleInput input = new ConsoleInput();
        Player player = new Player(new Vector2(5, 5), currentRenderer, input);
        BlocksController blocksController = new BlocksController(currentRenderer);


        MazeConfiguration mazeConfiguration = new MazeConfiguration(60, 120, 0.25f);
        MazeVisualizer mazeVisualizer = new MazeVisualizer(blocksController, currentRenderer);
        PrimsMazeGenerator primsMazeGenerator = new PrimsMazeGenerator();
        MazeGenerator mazeGenerator = new MazeGenerator(primsMazeGenerator);
        
        Update update = new Update(prevRenderer, currentRenderer);
        update.AddUpdateListener(input);
        update.AddUpdateListener(player);
        
        Map map = new Map(mazeGenerator, mazeVisualizer, mazeConfiguration);
        
        map.Init();
        
        GameRenderer gameRenderer = new GameRenderer(player, blocksController, renderer0);
        
        update.AddUpdateListener(gameRenderer);*/
        //gameRenderer.DrawGame();
        //update.StartUpdate();
        
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        
        
        ConsoleRenderer renderer0 = new ConsoleRenderer(Pallete.Colors); 
        ConsoleRenderer renderer1 = new ConsoleRenderer(Pallete.Colors); 
        
        ConsoleInput consoleInput = new ConsoleInput();
        
        ConsoleRenderer prevRenderer = renderer0; 
        ConsoleRenderer currentRenderer = renderer1; 
        
        DateTime lastFrameTime = DateTime.Now;

        while (true)
        {
            DateTime frameStartTime = DateTime.Now;
            float deltaTime = (float)(frameStartTime - lastFrameTime).TotalSeconds;
            
            consoleInput.Update(deltaTime);
            
            lastFrameTime = frameStartTime;
            
            if (!currentRenderer.Equals(prevRenderer)) currentRenderer.Render(); 

            ConsoleRenderer tmp = prevRenderer; 
            prevRenderer = currentRenderer; 
            currentRenderer = tmp; 
            currentRenderer.Clear(); 
            
            DateTime nextFrameTime = frameStartTime + TimeSpan.FromSeconds(targetFrameTime);
            DateTime endFrameTime = DateTime.Now;
            
            if (nextFrameTime > endFrameTime) 
            {
                Thread.Sleep((int)(nextFrameTime - endFrameTime).TotalMilliseconds);
            }
        }
        
    }
}

