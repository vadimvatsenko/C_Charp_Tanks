using C_Charp_Tanks.Fabrics;
using C_Charp_Tanks.Fabrics.BlocksFactory;
using C_Charp_Tanks.Fabrics.BulletsFactory;
using C_Charp_Tanks.Logic;
using C_Charp_Tanks.States;
using C_Charp_Tanks.Systems;
using C_Sharp_Maze_Generator.Maze;

namespace C_Charp_Tanks;

public class CompositionRoot
{
    public ConsoleInput ConsoleInput { get; }
    public ConsoleRenderer CurrentRenderer { get; private set; }
    public ConsoleRenderer PrevRenderer { get; private set; }
    private FabricController FabricController { get; }
    private CollisionSystem CollisionSystem { get; }
    public TankGameplayLogic TankGameplayLogic { get; }
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
        PrevRenderer = new ConsoleRenderer(Pallete.Colors);
        CurrentRenderer = new ConsoleRenderer(Pallete.Colors);
        
        unitFabric.SetFabricController(FabricController); // Устанавливаем зависимость после создания
        mazeCreator.SetFabricController(FabricController); // Устанавливаем зависимость после создания
        CollisionSystem.SetFabricController(FabricController); // Устанавливаем зависимость после создания

        // Системы и логика игры
        TankGameplayState tankGameplayState = new TankGameplayState(FabricController, CollisionSystem);
        TankGameplayLogic = new TankGameplayLogic(tankGameplayState, mazeCreator, FabricController);
    }
    
    public void SwapRenderers()
    {
        ConsoleRenderer temp = PrevRenderer;
        PrevRenderer = CurrentRenderer;
        CurrentRenderer = temp;
        CurrentRenderer.Clear();
    }
}