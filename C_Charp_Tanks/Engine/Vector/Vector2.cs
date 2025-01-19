namespace C_Charp_Tanks;

public struct Vector2
{
    public int X;
    public int Y;

    public static Vector2 Zero => new Vector2(0, 0);
    public static Vector2 Up => new Vector2(0, -1);
    public static Vector2 Down => new Vector2(0, 1);
    public static Vector2 Right => new Vector2(1, 0);
    public static Vector2 Left => new Vector2(-1, 0);
    
    public Vector2(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static Vector2 operator +(Vector2 left, Vector2 right) => new(left.X + right.X, left.Y + right.Y);
    public static Vector2 operator +(Vector2 left, int right) => new(left.X + right, left.Y + right);
    public static Vector2 operator -(Vector2 left, Vector2 right) => new(left.X - right.X, left.Y - right.Y);
    public static bool operator ==(Vector2 left, Vector2 right) => left.X == right.X && left.Y == right.Y;
    public static bool operator !=(Vector2 left, Vector2 right) => !(left == right);
    public static Vector2 operator *(Vector2 left, int numb) => new Vector2(left.X * numb, left.Y * numb);
    public static Vector2 operator /(Vector2 left, int numb) => new Vector2(left.X / numb, left.Y / numb);
    
    public float Magnitude() => MathF.Sqrt(X * X + Y * Y);
    
    public override bool Equals(object? obj)
    {
        if(obj is not Vector2 otherCell) return false;
        return X == otherCell.X && Y == otherCell.Y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}