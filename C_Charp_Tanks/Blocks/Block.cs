using System.Drawing;
using C_Charp_Tanks.Engine;
using C_Charp_Tanks.Renderer;

namespace C_Charp_Tanks.Blocks;
public abstract class Block : IUpdatable
{
    public BoxCollider2D Collider {get; protected set;}
    public Vector2 Position {get; protected set;}
    public char[,] View { get; protected set; }
    public char Symbol {get; protected set; }
    public BlockType Type {get; protected set;}
    public byte Color {get; protected set; }
    
    public Block(BlockType type, char symbol, Vector2 position)
    {
        Type = type;
        Symbol = symbol;
        View = new char[3, 3];
        Position = position;
        Collider = new BoxCollider2D(Position, new Vector2(3, 3));
        FillBlock();
    }

    protected void FillBlock()
    {
        for (int x = 0; x < View.GetLength(0); x++)
        {
            for (int y = 0; y < View.GetLength(1); y++)
            {
                View[x, y] = Symbol;
            }
        }
    }

    public virtual void Render(IRenderer renderer)
    {
        for (int x = 0; x < View.GetLength(0); x++)
        {
            for (int y = 0; y < View.GetLength(1); y++)
            {
                renderer.SetPixel(Position.X + x, Position.Y + y, View[x, y], Color);
            }
        }
    }

    public abstract void Update(double deltaTime);
    public abstract void GetDamage();

}