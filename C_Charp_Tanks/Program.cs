using C_Charp_Tanks;
using C_Charp_Tanks.Engine;
using C_Charp_Tanks.Engine.Renderer;
using C_Charp_Tanks.Fabrics;
using C_Charp_Tanks.Systems;
using C_Charp_Tanks.Units.Enemy;
using C_Charp_Tanks.Venicals;
using C_Charp_Tanks.Window;

public partial class Program
{
    const float targetFrameTime = 1f / 60f;

    public static void Main(string[] args)
    {

        WindowSettings windowSettings = new WindowSettings(92, 42);

        Console.OutputEncoding = System.Text.Encoding.UTF8;
        
        
        CompositionRoot compositionRoot = new CompositionRoot();
        MapConfig mapConfig = new MapConfig(90, 41);
        BaseRenderer renderer = new BaseRenderer();
        var backGroundLayer = renderer.CreateLayer(mapConfig.Width, mapConfig.Height);
        var tankLayer = renderer.CreateLayer(mapConfig.Width, mapConfig.Height);
        var enemiesLayer = renderer.CreateLayer(mapConfig.Width, mapConfig.Height);
        var bulletsLayer = renderer.CreateLayer(mapConfig.Width, mapConfig.Height);
        var uiLayer = renderer.CreateLayer(mapConfig.Width, mapConfig.Height);
        var wallLayer = renderer.CreateLayer(mapConfig.Width, mapConfig.Height);
        
        renderer.Fill(backGroundLayer, '.');

        Player player = new Player(new Vector2(6, 6), compositionRoot.FabricController, compositionRoot.ConsoleInput, new CollisionSystem(),
            tankLayer);

        Random random = new Random();
        
        List<Enemy> enemies = new List<Enemy>();
        
        for (int i = 0; i < 10; i++)
        {
            Enemy en = new Enemy(new Vector2(random.Next(0, mapConfig.Width), random.Next(0, mapConfig.Height)), compositionRoot.FabricController, new CollisionSystem(), enemiesLayer);
        }
        
        

        DateTime lastFrameTime = DateTime.Now;

        int counter = 0;

        while (true)
        {
            renderer.Clear(tankLayer);
            renderer.Clear(enemiesLayer);
            DateTime frameStartTime = DateTime.Now;
            float deltaTime = (float)(frameStartTime - lastFrameTime).TotalSeconds;

            // Обновляем ввод
            // compositionRoot.ConsoleInput.Update(deltaTime);

            // Рисуем новое состояние игры
            // compositionRoot.TankGameplayLogic.DrawNewState(deltaTime, compositionRoot.BaseRenderer);
            
            compositionRoot.TankGameplayLogic.Update(deltaTime);
            compositionRoot.ConsoleInput.Update(deltaTime);
            
            player.Render(renderer);
            enemies.ForEach(en => en.Render(renderer));
            
            char[,] frame = renderer.Compose(mapConfig.Width, mapConfig.Height, backGroundLayer, tankLayer);
            renderer.Render(frame);
            
            lastFrameTime = frameStartTime;
            
            // Рендерим кадр, если рендерер сменился
            /*if (!compositionRoot.CurrentRenderer.Equals(compositionRoot.PrevRenderer))
                compositionRoot.CurrentRenderer.Render();

            // Меняем рендереры местами
            compositionRoot.SwapRenderers();*/



            // Ограничение FPS
            DateTime nextFrameTime = frameStartTime + TimeSpan.FromSeconds(targetFrameTime);
            DateTime endFrameTime = DateTime.Now;

            if (nextFrameTime > endFrameTime)
            {
                Thread.Sleep((int)(nextFrameTime - endFrameTime).TotalMilliseconds);
            }
        }
    }

    
}

