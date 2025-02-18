using C_Charp_Tanks.Engine;
using C_Charp_Tanks.Fabrics;
using C_Charp_Tanks.Renderer;

namespace C_Charp_Tanks.Venicals;

public abstract class Shell: IUpdatable
{
    public Vector2 Position { get; protected set; }
    public BoxCollider2D BoxCollider2D { get; protected set; }
    public Vector2 Direction { get; protected set; }
    public char View { get; protected set; }
    public float Speed { get; protected set; }

    public Shell(Vector2 position, Vector2 dir)
    {
        Position = position;
        Direction = dir;
        BoxCollider2D = new BoxCollider2D(Position, Vector2.One);
    }
    
    public abstract void Update(double deltaTime);
    public abstract void Render(IRenderer renderer);
    public abstract void Destroy();
}