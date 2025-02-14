﻿using C_Charp_Tanks.Fabrics;
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
    public FabricController FabricController { get; }
    public CollisionSystem CollisionSystem { get; }
    public TankGameplayLogic TankGameplayLogic { get; }
    public CompositionRoot()
    {
        // Независимые объекты
        ConsoleInput = new ConsoleInput();
        BlocksFabric blocksFabric = new BlocksFabric();
        MazeCreator mazeCreator = new MazeCreator();

        // Рендеринг
        PrevRenderer = new ConsoleRenderer(Pallete.Colors);
        CurrentRenderer = new ConsoleRenderer(Pallete.Colors);

        // Фабрики и контроллеры
        UnitFabric unitFabric = new UnitFabric(ConsoleInput, mazeCreator);
        FabricController = new FabricController(unitFabric, blocksFabric);
        unitFabric.SetFabricController(FabricController); // Устанавливаем зависимость после создания
        mazeCreator.SetFabricController(FabricController);

        // Системы и логика игры
        CollisionSystem = new CollisionSystem(FabricController);
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