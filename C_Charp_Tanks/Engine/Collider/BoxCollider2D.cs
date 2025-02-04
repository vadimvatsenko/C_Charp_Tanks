namespace C_Charp_Tanks.Engine;

public class BoxCollider2D : Collider2D
{
    public Vector2 Size { get; private set; }
    
    public BoxCollider2D(Vector2 position, Vector2 size) : base(position)
    {
        Size = size;
    }

    public override bool IsColliding(Collider2D other)
    {
        if (other is BoxCollider2D box)
        {
            return !(Position.X + Size.X <= box.Position.X || // Лево одного за правым другого
                     Position.X >= box.Position.X + box.Size.X || // Право одного за левым другого
                     Position.Y + Size.Y <= box.Position.Y || // Низ одного выше верха другого
                     Position.Y >= box.Position.Y + box.Size.Y); // Верх одного ниже низа другого
        }

        throw new NotSupportedException("Intersection with this collider type is not supported.");
    }
}