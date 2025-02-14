namespace C_Charp_Tanks.Venicals;

public class UnitConfig
{
    public Vector2 Position { get; private set; }
    public char[] View { get; private set; }
    public UnitType UnitType { get; private set; }

    public UnitConfig(Vector2 position, char[] view, UnitType unitType)
    {
        Position = position;
        View = view;
        UnitType = unitType;
    }
}