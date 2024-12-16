using System;
using C_Charp_Tanks;
using C_Charp_Tanks.Logics;
using C_Charp_Tanks.MazeGenerator;
using C_Charp_Tanks.Venicals;


public class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        
        ConsoleRenderer renderer = new ConsoleRenderer(Pallete.colors);
        
        Update update = new Update();
        GameData gameData = new GameData();
        Tank tank = new Tank(gameData);
        
        PlayerInput input = new PlayerInput(tank);
        
        input.RegisterListener(tank);
        update.RegisterListener(tank);
        update.RegisterListener(input);
        
        PlayerView playerView = new PlayerView(tank, renderer, gameData);
        
        update.RegisterListener(playerView);
        
        update.Start();

        /*ConsoleRenderer renderer = new ConsoleRenderer(Pallete.colors);
        
        MazeGenerator mazeGenerator = new MazeGenerator(new PrimsMazeGenerator());
        MazeVisualizer mazeVisualizer = new MazeVisualizer(renderer);
        MazeConfiguration mazeConfiguration = new MazeConfiguration(14, 14, 0.1f);
        Map map = new Map(mazeGenerator, mazeVisualizer, mazeConfiguration);
        map.Init();*/
        
    }
}