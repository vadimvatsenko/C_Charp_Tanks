using System.Drawing;
using C_Charp_Tanks.Renderer;

namespace C_Charp_Tanks.Blocks;

public class IndestructibleBlock : Block, IUpdatable
{
    public IndestructibleBlock(Vector2 position, IRenderer renderer, BlockType type) : base(position, renderer, type)
    {
    }

    public void Update(double deltaTime)
    {
        int length = 30;
        
        for (int i = 0; i < length; i++) Renderer.SetPixel(i, 0, Symbols.Wall, 7);
        
        for (int i = 0; i <= length; i++) Renderer.SetPixel(i, length, Symbols.Wall, 7);
        
        for (int i = 0; i < length; i++) Renderer.SetPixel(0, i, Symbols.Wall, 7);
        
        for (int i = 0; i < length; i++) Renderer.SetPixel(length, i, Symbols.Wall, 7);
        
        Renderer.Render();
    }
}