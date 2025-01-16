namespace C_Charp_Tanks;

public class ConsoleInput : IConsoleInput, IUpdatable
{
    public event Action? MoveUp;
    public event Action? MoveDown;
    public event Action? MoveLeft;
    public event Action? MoveRight;
    public void Update(double deltaTime)
    {
        ConsoleKeyInfo keyInfo;

        if (Console.KeyAvailable)
        {
            keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow or ConsoleKey.W:
                    MoveUp?.Invoke();
                    break;
                case ConsoleKey.DownArrow or ConsoleKey.S:
                    MoveDown?.Invoke();
                    break;
                case ConsoleKey.LeftArrow or ConsoleKey.A:
                    MoveLeft?.Invoke();
                    break;
                case ConsoleKey.RightArrow or ConsoleKey.D:
                    MoveRight?.Invoke();
                    break;
            }
        }
    }
}