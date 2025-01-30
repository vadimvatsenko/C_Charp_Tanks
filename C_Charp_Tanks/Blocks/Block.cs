using C_Charp_Tanks.Engine;
using C_Charp_Tanks.Renderer;

namespace C_Charp_Tanks.Blocks;

public abstract class Block
{
    protected Vector2 Position;
    protected char[,] View;
    protected BoxCollider2D Collider;
    protected IRenderer Renderer;
    protected BlockType Type;

    public Block(Vector2 position, IRenderer renderer, BlockType type)
    {
        Position = position;
        Renderer = renderer;
        Type = type;
    }
        
    
}