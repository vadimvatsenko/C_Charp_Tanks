namespace C_Charp_Tanks;

public class ConsoleInput : IConsoleInput, IUpdatable
{
    private Dictionary<ConsoleKey, bool> _keyState; // словарь для отслеживания состояния клавиш
    public event Action? MoveUp;
    public event Action? MoveDown;
    public event Action? MoveLeft;
    public event Action? MoveRight;

    public ConsoleInput()
    {
        _keyState = new Dictionary<ConsoleKey, bool>();
    }

    public void Update(double deltaTime)
    {
        ConsoleKeyInfo keyInfo;

        if (Console.KeyAvailable)
        {
            keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow or ConsoleKey.W:
                    if (!_keyState.ContainsKey(keyInfo.Key) || !_keyState[keyInfo.Key])
                    {
                        MoveUp?.Invoke();
                        _keyState[ConsoleKey.DownArrow] = true;
                    }

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

            // При отпускании клавиши сбрасывайте состояние
            if (keyInfo.Key == ConsoleKey.UpArrow || keyInfo.Key == ConsoleKey.W ||
                keyInfo.Key == ConsoleKey.DownArrow || keyInfo.Key == ConsoleKey.S ||
                keyInfo.Key == ConsoleKey.LeftArrow || keyInfo.Key == ConsoleKey.A ||
                keyInfo.Key == ConsoleKey.RightArrow || keyInfo.Key == ConsoleKey.D)
            {
                if (_keyState.ContainsKey(keyInfo.Key) && _keyState[keyInfo.Key])
                {
                    _keyState[keyInfo.Key] = false; // Сбрасываем состояние, если клавиша отпущена
                }
            }
        }
        
    }
}