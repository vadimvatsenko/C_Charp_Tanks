﻿namespace C_Charp_Tanks.MazeGenerator;

public class MazeCreator
{
    private MazeConfiguration _mazeConfiguration;
    private IMazeAlgorithm _mazeAlgorithm;
    private MazeGenerator _mazeGenerator;
    public MazeVisualizer _mazeVisualizer { get; private set; }
    private Map _map;

    private int _mazeWidth;
    private int _mazeHeight;

    private float _gapsChance = 0.0f;

    public MazeCreator()
    {
        _mazeWidth = Console.WindowWidth <= 120 ? Console.WindowWidth - 2 : 120;
        _mazeHeight = Console.WindowHeight <= 60 ? Console.WindowHeight - 2 : 60;
        
    }

    public void Initialize()
    {
        _mazeConfiguration = new MazeConfiguration(60, 36, _gapsChance);
        _mazeAlgorithm = new PrimsMazeGenerator();
        _mazeVisualizer = new MazeVisualizer();
        _mazeGenerator = new MazeGenerator(_mazeAlgorithm);
        _map = new Map(_mazeGenerator, _mazeVisualizer, _mazeConfiguration);
        _map.Init();
    }
}