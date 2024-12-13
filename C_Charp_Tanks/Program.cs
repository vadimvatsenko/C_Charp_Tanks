using System;
using C_Charp_Tanks;
using C_Charp_Tanks.Logics;
using C_Charp_Tanks.Venicals;

public class Program
{
    public static void Main(string[] args)
    {
        PlayerInput input = new PlayerInput();
        ConsoleRenderer renderer = new ConsoleRenderer(Pallete.colors);
        
        Update update = new Update();
        Tank tank = new Tank();
        GameData gameData = new GameData();
        
        input.RegisterListener(tank);
        update.RegisterListener(tank);
        update.RegisterListener(input);
        
        PlayerView playerView = new PlayerView(tank, renderer, gameData);
        
        update.RegisterListener(playerView);
        
        update.Start();
        
    }
}