using C_Charp_Tanks.C_Charp_Tanks.Engine.Collider;
using C_Charp_Tanks.Engine;
using C_Charp_Tanks.Engine.Renderer;

namespace C_Charp_Tanks.Blocks;
public abstract class Block : IUpdatable
{
    protected readonly char[,] Layer;
    public BoxCollider2D Collider {get; protected set;}
    public Vector2 Position {get; protected set;}
    public char[,] View { get; protected set; }
    public char Symbol {get; protected set; }
    public BlockType Type {get; protected set;}
    public byte Color {get; protected set; }
    
    public Block(BlockType type, char symbol, Vector2 position,  char[,] layer)
    {
        Type = type;
        Symbol = symbol;
        View = new char[3, 3];
        Position = position;
        Collider = new BoxCollider2D(Position, new Vector2(3, 3));
        Layer = layer;
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

    public virtual void Render(BaseRenderer renderer)
    {
        for (int x = 0; x < View.GetLength(0); x++)
        {
            for (int y = 0; y < View.GetLength(1); y++)
            {
                renderer.DrawChar(Layer, Position.X + x, Position.Y + y, View[x, y]);
            }
        }
    }

    public abstract void Update(double deltaTime);
    public abstract void GetDamage();

}