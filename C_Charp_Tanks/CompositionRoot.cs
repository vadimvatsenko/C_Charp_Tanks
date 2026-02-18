using C_Charp_Tanks.Engine;
using C_Charp_Tanks.Engine.Renderer;
using C_Charp_Tanks.Fabrics;
using C_Charp_Tanks.Fabrics.BlocksFactory;
using C_Charp_Tanks.Logic;
using C_Charp_Tanks.States;
using C_Charp_Tanks.Systems;
using C_Sharp_Maze_Generator.Maze;

namespace C_Charp_Tanks;

public class CompositionRoot
{
    public ConsoleInput ConsoleInput { get; }
    public BaseRenderer BaseRenderer { get; private set; }
    
    private FabricController FabricController { get; }
    private CollisionSystem CollisionSystem { get; }
    public TankGameplayLogic TankGameplayLogic { get; }

    public MapConfig mapConfig = new MapConfig(90, 41);
    
    
    
    public CompositionRoot()
    {
        // Независимые объекты
        ConsoleInput = new ConsoleInput();
        CollisionSystem = new CollisionSystem();
        
        // фабрики
        MazeCreator mazeCreator = new MazeCreator();
        BlocksFabric blocksFabric = new BlocksFabric();
        BulletsFabric bulletsFabric = new BulletsFabric();
        UnitFabric unitFabric = new UnitFabric(ConsoleInput, CollisionSystem);
        
        FabricController = new FabricController(unitFabric, blocksFabric, bulletsFabric);
        
        // Рендеринг
        BaseRenderer = new BaseRenderer();
        var tankLayer = BaseRenderer.CreateLayer(mapConfig.Width, mapConfig.Height);
        var bulletsLayer = BaseRenderer.CreateLayer(mapConfig.Width, mapConfig.Height);
        
        unitFabric.SetFabricController(FabricController); // Устанавливаем зависимость после создания
        mazeCreator.SetFabricController(FabricController); // Устанавливаем зависимость после создания
        CollisionSystem.SetFabricController(FabricController); // Устанавливаем зависимость после создания

        // Системы и логика игры
        TankGameplayState tankGameplayState = new TankGameplayState(FabricController, CollisionSystem, mapConfig);
        TankGameplayLogic = new TankGameplayLogic(tankGameplayState, mazeCreator, FabricController);
    }
    
}