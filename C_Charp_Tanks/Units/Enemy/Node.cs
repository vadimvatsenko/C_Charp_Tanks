namespace C_Charp_Tanks.Venicals.Enemy;

public class Node
{
    public Vector2 Position;
    public int Cost = 10;
    public int Estimate;
    public int Value;
    public Node Parent;

    public Node(Vector2 position)
    {
        Position = position;
    }

    public void CalculateEstimate(Vector2 targetPosition)
    {
        Estimate = Math.Abs(targetPosition.X - Position.X) + Math.Abs(targetPosition.Y - Position.Y);
    }

    public void CalculateValue()
    {
        Value = Cost + Estimate * Value;
    }

    public override bool Equals(object? obj)
    {
        if(obj is not Node node) return false;
        return Position.Equals(node.Position);
    }

    public override int GetHashCode()
    {
        return Position.GetHashCode();
    }
}