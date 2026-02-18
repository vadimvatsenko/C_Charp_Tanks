namespace C_Charp_Tanks.C_Charp_Tanks.Engine.Collider;

public abstract class Collider2D
{
    public Vector2 Position { get; set; }

    public Collider2D(Vector2 position)
    {
        Position = position;
    }
    
    public abstract bool IsColliding(Collider2D other);
}