using C_Charp_Tanks.Renderer;
using C_Charp_Tanks.Venicals;

namespace C_Charp_Tanks;

public class Tank: Unit, IConsoleInput
{
    public Vector2 position { get; private set; }
    
    public Tank()
    {
        this.position = Vector2.Zero + 5;
    }
    
    public void TryToMoveUp()
    {
        position += Vector2.Up;
    }

    public void TryToMoveDown()
    {
        position += Vector2.Down;
    }

    public void TryToMoveLeft()
    {
        position += Vector2.Left;
    }

    public void TryToMoveRight()
    {
        position += Vector2.Right;
    }

    public void TryToShoot()
    {
        Console.WriteLine("Tank Shoot");
    }
}