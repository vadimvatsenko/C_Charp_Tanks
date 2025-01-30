using C_Charp_Tanks.Engine;
using C_Charp_Tanks.Renderer;

namespace C_Charp_Tanks.Blocks;

public abstract class Block
{
    public char[,] View { get; protected set; }
    protected char Symbol;
    protected BoxCollider2D Collider;
    protected BlockType Type;

    public Block(BlockType type, char symbol)
    {
        Type = type;
        Symbol = symbol;
        View = new char[3, 3];
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
}