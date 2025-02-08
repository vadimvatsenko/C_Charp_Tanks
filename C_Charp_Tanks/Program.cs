using C_Charp_Tanks;
using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.Engine.Window;
using C_Charp_Tanks.Logic;
using C_Charp_Tanks.MazeGenerator;
using C_Charp_Tanks.Renderer;
using C_Charp_Tanks.States;
using C_Charp_Tanks.Venicals;
using C_Charp_Tanks.Venicals.Enemy;

public class Program
{
    const float targetFrameTime = 1f / 60f;
    public static void Main(string[] args)
    {
        //WindowSettings windowSettings = new WindowSettings(60, 120);
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Random rand = new Random();
        
        ConsoleRenderer renderer0 = new ConsoleRenderer(Pallete.Colors); 
        ConsoleRenderer renderer1 = new ConsoleRenderer(Pallete.Colors); 
        
        ConsoleInput consoleInput = new ConsoleInput();
        
        ConsoleRenderer prevRenderer = renderer0; 
        ConsoleRenderer currentRenderer = renderer1; 
        
        
        //
        MazeConfiguration mazeConfiguration = new MazeConfiguration(60, 120, 1f);
        IMazeAlgorithm mazeAlgorithm = new PrimsMazeGenerator();
        MazeGenerator mazeGenerator = new MazeGenerator(mazeAlgorithm);
        MazeVisualizer mazeVisualizer = new MazeVisualizer();
        Map map = new Map(mazeGenerator, mazeVisualizer, mazeConfiguration);
        map.Init();
        //

        Vector2 playerPosition = mazeVisualizer.EmptyFields[rand.Next(0, mazeVisualizer.EmptyFields.Count)];
        Vector2 enemyPosition = mazeVisualizer.EmptyFields[rand.Next(0, mazeVisualizer.EmptyFields.Count)];
        Vector2 targetPosition = mazeVisualizer.EmptyFields[rand.Next(0, mazeVisualizer.EmptyFields.Count)];
        
        
        
        Player player = new Player(playerPosition, consoleInput);
        Enemy enemy = new Enemy(enemyPosition, targetPosition);
        
        UnitObjects.Instance.AddObject(player);
        UnitObjects.Instance.AddObject(enemy);
        
        //
        
        //
        TankGameplayState tankGameplayState = new TankGameplayState();
        TankGameplayLogic tankGameplayLogic = new TankGameplayLogic(tankGameplayState);
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

