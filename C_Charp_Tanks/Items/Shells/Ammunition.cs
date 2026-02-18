using C_Charp_Tanks.C_Charp_Tanks.Engine.Collider;
using C_Charp_Tanks.Engine.Renderer;

namespace C_Charp_Tanks.Items.Shells;

public abstract class Ammunition: IUpdatable
{
    public Vector2 Position { get; protected set; }
    public BoxCollider2D Collider { get; protected set; }
    public Vector2 Direction { get; protected set; }
    public char View { get; protected set; }
    public float Speed { get; protected set; }

    public Ammunition(Vector2 position, Vector2 dir)
    {
        Position = position;
        Direction = dir;
        Collider = new BoxCollider2D(Position, Vector2.One);
    }
    
    public abstract void Update(double deltaTime);
    public abstract void Render(BaseRenderer renderer);
}