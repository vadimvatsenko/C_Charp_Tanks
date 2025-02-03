using C_Charp_Tanks;
using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.Logic;
using C_Charp_Tanks.MazeGenerator;
using C_Charp_Tanks.Renderer;
using C_Charp_Tanks.States;
using C_Charp_Tanks.Venicals;

public class Program
{
    const float targetFrameTime = 1f / 60f;
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        
        ConsoleRenderer renderer0 = new ConsoleRenderer(Pallete.Colors); 
        ConsoleRenderer renderer1 = new ConsoleRenderer(Pallete.Colors); 
        
        ConsoleInput consoleInput = new ConsoleInput();

        Player player = new Player(new Vector2(9, 9), consoleInput);
        TankGameplayState tankGameplayState = new TankGameplayState(player);
        TankGameplayLogic tankGameplayLogic = new TankGameplayLogic(tankGameplayState);
        
        ConsoleRenderer prevRenderer = renderer0; 
        ConsoleRenderer currentRenderer = renderer1; 
        
        BlocksController blocksController = new BlocksController(currentRenderer);
        
        //
        MazeConfiguration mazeConfiguration = new MazeConfiguration(60, 120, 0.25f);
        IMazeAlgorithm mazeAlgorithm = new PrimsMazeGenerator();
        MazeGenerator mazeGenerator = new MazeGenerator(mazeAlgorithm);
        MazeVisualizer mazeVisualizer = new MazeVisualizer(blocksController, currentRenderer);
        Map map = new Map(mazeGenerator, mazeVisualizer, mazeConfiguration);
        map.Init();
        //
        
        DateTime lastFrameTime = DateTime.Now;

        while (true)
        {
            DateTime frameStartTime = DateTime.Now;
            float deltaTime = (float)(frameStartTime - lastFrameTime).TotalSeconds;
            
            consoleInput.Update(deltaTime);
            
            tankGameplayLogic.DrawNewState(deltaTime, currentRenderer);
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

