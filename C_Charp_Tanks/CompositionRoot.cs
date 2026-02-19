using C_Charp_Tanks.Engine;
using C_Charp_Tanks.Engine.Renderer;
using C_Charp_Tanks.Fabrics;
using C_Charp_Tanks.Fabrics.BlocksFactory;
using C_Charp_Tanks.Logic;
using C_Charp_Tanks.Maze;
using C_Charp_Tanks.States;
using C_Charp_Tanks.Systems;

namespace C_Charp_Tanks;

public class CompositionRoot
{
    public ConsoleInput ConsoleInput { get; }
    public BaseRenderer BaseRenderer { get; private set; }
    public FabricController FabricController { get; private set; }
    private CollisionSystem CollisionSystem { get; }
    public TankGameplayLogic TankGameplayLogic { get; }

    public MapConfig mapConfig = new MapConfig(90, 41);
    
    
    
    public CompositionRoot()
    {
        // Независимые объекты
        ConsoleInput = new ConsoleInput();
        CollisionSystem = new CollisionSystem();
        
        // Рендеринг
        BaseRenderer = new BaseRenderer();
        var backGroundLayer = BaseRenderer.CreateLayer(mapConfig.Width, mapConfig.Height);
        var tankLayer = BaseRenderer.CreateLayer(mapConfig.Width, mapConfig.Height);
        var enemiesLayer = BaseRenderer.CreateLayer(mapConfig.Width, mapConfig.Height);
        var bulletsLayer = BaseRenderer.CreateLayer(mapConfig.Width, mapConfig.Height);
        var uiLayer = BaseRenderer.CreateLayer(mapConfig.Width, mapConfig.Height);
        var wallLayer = BaseRenderer.CreateLayer(mapConfig.Width, mapConfig.Height);
        
        BaseRenderer.Fill(backGroundLayer, '*');
        
        // фабрики
        MazeCreator mazeCreator = new MazeCreator(wallLayer);
        BlocksFabric blocksFabric = new BlocksFabric();
        BulletsFabric bulletsFabric = new BulletsFabric(bulletsLayer);
        UnitFabric unitFabric = new UnitFabric(ConsoleInput, CollisionSystem, tankLayer, enemiesLayer);
        
        FabricController = new FabricController(unitFabric, blocksFabric, bulletsFabric);
        
        unitFabric.SetFabricController(FabricController); // Устанавливаем зависимость после создания
        mazeCreator.SetFabricController(FabricController); // Устанавливаем зависимость после создания
        CollisionSystem.SetFabricController(FabricController); // Устанавливаем зависимость после создания

        // Системы и логика игры
        TankGameplayState tankGameplayState = new TankGameplayState(FabricController, CollisionSystem, mapConfig, uiLayer);
        TankGameplayLogic = new TankGameplayLogic(tankGameplayState, mazeCreator, FabricController, mapConfig, tankLayer);
        
        
        var frame = BaseRenderer.Compose(mapConfig.Width, mapConfig.Height, tankLayer, enemiesLayer, uiLayer, wallLayer, bulletsLayer);
        BaseRenderer.Render(frame);
        
        TankGameplayLogic.DrawNewState(1, BaseRenderer);
        
        
    }
    
}