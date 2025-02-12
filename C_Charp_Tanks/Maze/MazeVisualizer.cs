﻿using C_Charp_Tanks;
using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.Fabrics.BlocksFactory;
using C_Charp_Tanks.Renderer;

namespace C_Sharp_Maze_Generator.Maze;

public class MazeVisualizer
{
    private Random _random = new Random();
    public List<Vector2> EmptyFields { get; private set; }
    public BlocksFabric _blocksFabric { get; private set; }
    
    private int _step = 3;
    public void Visualise(bool[,] maze)
    {
        BuildWalls(maze);
        BuildWallsAround(maze);
    }

    public MazeVisualizer(BlocksFabric blocksFabric) 
    {
        _blocksFabric = blocksFabric;
        EmptyFields = new List<Vector2>();
    }
    
    private void BuildWalls(bool[,] maze)
    {
        int mazeHeight = maze.GetLength(1);
        int mazeWidth = maze.GetLength(0);

        // Определяем начальную позицию отрисовки (по центру окна)
        int startX = Console.WindowWidth / 2 - mazeWidth / 2 * _step;
        int startY = Console.WindowHeight / 2 - mazeHeight / 2 * _step;
        
        for (int i = 0; i < mazeWidth - 1; i ++)
        for (int j = 0; j < mazeHeight - 1; j ++)
        {
            if (maze[i, j] == false)
            {
                int randomBlock = _random.Next(2);
                if (randomBlock == 0)
                {
                    /*WaterBlock waterBlock = new WaterBlock(BlockType.Water, Symbols.Wall, new Vector2(startX + i * _step + _step , j * _step + _step));
                    _blocksFabric.AddBlock(waterBlock);*/
                    
                    WaterBlock waterBlock = new WaterBlock(BlockType.Water, Symbols.Wall, new Vector2(i * _step + _step + startX, j * _step + _step));
                    _blocksFabric.AddBlock(waterBlock);
                }
                else
                {
                    /*DestructibleBlock destructibleBlock = new DestructibleBlock(BlockType.Destructible, Symbols.Wall, new Vector2(startX + i * _step + _step , j * _step + _step)); 
                    _blocksFabric.AddBlock(destructibleBlock);*/
                    
                    DestructibleBlock destructibleBlock = new DestructibleBlock(BlockType.Destructible, Symbols.Wall, new Vector2(i * _step + _step + startX, j * _step + _step)); 
                    _blocksFabric.AddBlock(destructibleBlock);
                }
            }

            else
            {
                EmptyFields.Add(new Vector2(i * _step + _step + startX, j * _step + _step));
            }
        }
    }

    private void BuildWallsAround(bool[,] maze)
    {
        int mazeHeight = maze.GetLength(1);
        int mazeWidth = maze.GetLength(0);

        // Определяем начальную позицию отрисовки (по центру окна)
        int startX = Console.WindowWidth / 2 - mazeWidth / 2 * _step;
        int startY = Console.WindowHeight / 2 - mazeHeight / 2 * _step;
        
       // вертикаль
        for (int i = 0; i <= mazeHeight * _step; i += _step)
        {
            IndestructibleBlock indestructibleBlockLeft =
                new IndestructibleBlock(BlockType.Indestructible, Symbols.Wall, new Vector2(startX, i));
            _blocksFabric.AddBlock(indestructibleBlockLeft);

            IndestructibleBlock indestructibleBlockRight =
                new IndestructibleBlock(BlockType.Indestructible, Symbols.Wall, new Vector2(startX + mazeWidth * _step, i));
            _blocksFabric.AddBlock(indestructibleBlockRight);

        }
        
        // горизонталь
        
        for (int j = 0; j <= mazeWidth * _step; j += _step)
        {
            IndestructibleBlock indestructibleBlockLeft =
               new IndestructibleBlock(BlockType.Indestructible, Symbols.Wall, new Vector2(startX + j, 0));
            _blocksFabric.AddBlock(indestructibleBlockLeft);

            IndestructibleBlock indestructibleBlockRight =
                new IndestructibleBlock(BlockType.Indestructible, Symbols.Wall, new Vector2(startX + j, mazeHeight * _step));
            _blocksFabric.AddBlock(indestructibleBlockRight);
        }
    }
}