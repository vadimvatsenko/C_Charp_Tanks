namespace C_Charp_Tanks;

public interface IConsoleInput
{
    public event Action MoveUp;
    public event Action MoveDown;
    public event Action MoveLeft;
    public event Action MoveRight;
    
}