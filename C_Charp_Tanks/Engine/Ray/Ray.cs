namespace C_Charp_Tanks.Engine.Ray;

public struct Ray
{
    public Vector2 Origin { get; set; }
    public Vector2 Direction { get; set; }
    public float Length { get; set; }

    public Ray(Vector2 origin, Vector2 direction, float length)
    {
        Origin = origin;
        Direction = direction;
        Length = length;
    }
}