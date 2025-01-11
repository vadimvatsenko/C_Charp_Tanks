using C_Charp_Tanks.Venicals;

namespace C_Charp_Tanks;

public class PlayerInput : IUpdatable
{
    private readonly HashSet<IConsoleInput> _listeners = new();

    public PlayerInput(IConsoleInput listener)
    {
        RegisterListener(listener);
    }
    
    public void RegisterListener(IConsoleInput listener)
    {
        if(!_listeners.Contains(listener)) _listeners.Add(listener);
    }

    public void UnregisterListener(IConsoleInput listener)
    {
        if(_listeners.Contains(listener)) _listeners.Remove(listener);
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
                    foreach (IConsoleInput listener in _listeners) listener.TryToMoveUp();
                    break;
                case ConsoleKey.DownArrow or ConsoleKey.S:
                    foreach (IConsoleInput listener in _listeners) listener.TryToMoveDown();
                    break;
                case ConsoleKey.LeftArrow or ConsoleKey.A:
                    foreach (IConsoleInput listener in _listeners) listener.TryToMoveLeft();
                    break;
                case ConsoleKey.RightArrow or ConsoleKey.D:
                    foreach (IConsoleInput listener in _listeners) listener.TryToMoveRight();
                    break;
                case ConsoleKey.Spacebar:
                    foreach (IConsoleInput listener in _listeners) listener.TryToShoot();
                    break;
            }
        }
    }
}