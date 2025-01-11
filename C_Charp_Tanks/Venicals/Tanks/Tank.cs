using C_Charp_Tanks.Renderer;
using C_Charp_Tanks.Venicals;

namespace C_Charp_Tanks;

public class Tank: Unit, IConsoleInput
{
    public Vector2 Position { get; private set; }
    
    public char[,] TankElement { get; private set; }

    public byte Color { get; private set; } = 7;
    
    public int Layer { get; private set; } = 0;
    
    public Tank()
    {
  
        Position = Vector2.Zero + 5;
        
        TankElement = GameData.Instance.Tank;
    }
    
    public void TryToMoveUp()
    {
        ChackNextStep(VenicalsDirection.Up);
    }

    public void TryToMoveDown()
    {
        ChackNextStep(VenicalsDirection.Down);
    }

    public void TryToMoveLeft()
    {
        ChackNextStep(VenicalsDirection.Left);
    }

    public void TryToMoveRight()
    {
        ChackNextStep(VenicalsDirection.Right);
    }

    public void TryToShoot()
    {
        Console.WriteLine("Tank Shoot");
    }

    private void ChackNextStep(VenicalsDirection venicalsDirection)
    {
        switch (venicalsDirection)
        {
            case VenicalsDirection.Up:
                Position += Vector2.Up;
                break;
            case VenicalsDirection.Down:
                Position += Vector2.Down;
                break;
            case VenicalsDirection.Left:
                Position += Vector2.Left;
                break;
            case VenicalsDirection.Right:
                Position += Vector2.Right;
                break;
        }
    }
    
}