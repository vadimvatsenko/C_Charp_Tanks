using C_Charp_Tanks;
using C_Charp_Tanks.Logic;
using C_Charp_Tanks.MazeGenerator;
using C_Charp_Tanks.States;

public class Program
{
    const float targetFrameTime = 1f / 60f;
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        
        ConsoleRenderer renderer0 = new ConsoleRenderer(Pallete.Colors); 
        ConsoleRenderer renderer1 = new ConsoleRenderer(Pallete.Colors); 
        
        ConsoleInput consoleInput = new ConsoleInput();
        
        ConsoleRenderer prevRenderer = renderer0; 
        ConsoleRenderer currentRenderer = renderer1; 
        
        MazeCreator mazeCreator = new MazeCreator();
        mazeCreator.Initialize(); // 
        
        UnitFabric unitFabric = new UnitFabric(consoleInput, mazeCreator);
        
        TankGameplayState tankGameplayState = new TankGameplayState();
        TankGameplayLogic tankGameplayLogic = new TankGameplayLogic(tankGameplayState, mazeCreator, unitFabric);
        
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

