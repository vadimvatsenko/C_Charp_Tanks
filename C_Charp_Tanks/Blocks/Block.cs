using System.Drawing;
using C_Charp_Tanks.Engine;
using C_Charp_Tanks.Renderer;

namespace C_Charp_Tanks.Blocks;

public abstract class Block : IUpdatable
{
    public char[,] View { get; protected set; }
    protected char Symbol;
    protected BoxCollider2D Collider;
    protected BlockType Type;
    protected Vector2 Position;
    protected IRenderer Renderer;
    protected byte Color;

    public Block(BlockType type, char symbol, Vector2 position, IRenderer renderer)
    {
        Type = type;
        Symbol = symbol;
        View = new char[3, 3];
        Position = position;
        Renderer = renderer;
        Collider = new BoxCollider2D(Position, new Vector2(3, 3));
        FillBlock();
    }

    private void FillBlock()
    {
        for (int x = 0; x < View.GetLength(0); x++)
        {
            for (int y = 0; y < View.GetLength(1); y++)
            {
                View[x, y] = Symbol;
            }
        }
    }

    public void RendererBlocks()
    {
        for (int x = 0; x < View.GetLength(0); x++)
        {
            for (int y = 0; y < View.GetLength(1); y++)
            {
                Renderer.SetPixel(Position.X + x, Position.Y + y, View[x, y], Color);
            }
        }
    }

    public void Update(double deltaTime)
    {
        
    }
}