using C_Charp_Tanks.Renderer;
using C_Charp_Tanks.Venicals;

namespace C_Charp_Tanks;

public class Tank: Unit, IConsoleInput
{
    public Vector2 position { get; private set; }
    private GameData _gameData;
    
    public Tank(GameData gameData)
    {
        _gameData = gameData;
        position = Vector2.Zero + 5;
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
                Vector2 nextStep = position + Vector2.Up;
                if (_gameData.Level[nextStep.Y, nextStep.X] != '#') position += Vector2.Up;
                break;
            case VenicalsDirection.Down:
                position += Vector2.Down;
                break;
            case VenicalsDirection.Left:
                position += Vector2.Left;
                break;
            case VenicalsDirection.Right:
                position += Vector2.Right;
                break;
        }
    }
    
}