﻿using C_Charp_Tanks.Venicals;

namespace C_Charp_Tanks.Blocks;

public class BlocksController 
{
    public List<Block> Blocks {get; private set;}
    private ConsoleRenderer _consoleRenderer;
    
    public event Action? IsGenerateMap;

    public BlocksController(ConsoleRenderer consoleRenderer)
    {
        Blocks = new List<Block>();
        _consoleRenderer = consoleRenderer;
    }
    
    public void AddBlock(Block block)
    {
        Blocks.Add(block);
    }

    public void RemoveBlock(Block block)
    {
        Blocks.Remove(block);
    }
    
    public void GenerateBlocks()
    {
        foreach (var block in Blocks)
        {
            block.RendererBlocks();
        }
        
        _consoleRenderer.Render();
        
        IsGenerateMap?.Invoke();
    }
    
}