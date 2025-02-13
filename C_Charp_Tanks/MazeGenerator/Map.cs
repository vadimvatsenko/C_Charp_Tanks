﻿namespace C_Charp_Tanks.MazeGenerator;

public class Map
{
    public event Action onMapChanged;
    
    private MazeGenerator _mazeGenerator;
    private MazeConfiguration _mazeConfiguration;
    private MazeVisualizer _mazeVisualizer;
    public bool[,] map { get; private set; }  // false - wall, true - road

    public Map(MazeConfiguration mazeConfiguration, MazeVisualizer mazeVisualizer )
    {
        _mazeGenerator = new MazeGenerator(new PrimsMazeGenerator());
        _mazeConfiguration = mazeConfiguration;
        _mazeVisualizer = mazeVisualizer;
        
        GenerateMaze(
            _mazeConfiguration.Width, 
            _mazeConfiguration.Height, 
            _mazeConfiguration.GapsChance);
    }

    public void GenerateMaze(int width, int height, float gaps)
    {
        map = _mazeGenerator.Generate(width, height, gaps);
        _mazeVisualizer.Visualise(map);
        
        onMapChanged?.Invoke();
    }
}