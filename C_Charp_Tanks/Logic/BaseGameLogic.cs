using System.Drawing;
using C_Charp_Tanks.States;

namespace C_Charp_Tanks.Logic;

public abstract class BaseGameLogic : IConsoleInput
{
    protected BaseGameState? CurrentState { get; private set; }
    protected float Time {get; private set;}
    protected int ScreenWidth {get; private set;}
    protected int ScreenHeight {get; private set;}
    public event Action? MoveUp;
    public event Action? MoveDown;
    public event Action? MoveLeft;
    public event Action? MoveRight;
    public event Action? Shoot;
    public abstract void Update(float deltaTime);

    public void ChangeState(BaseGameState state)
    {
        CurrentState?.Reset();
        CurrentState = state;
    }

    public void DrawNewState(float deltaTime, ConsoleRenderer renderer)
    {
        Time += deltaTime;
        ScreenWidth = renderer.width;
        ScreenHeight = renderer.height;
        CurrentState?.Update(deltaTime);
        CurrentState?.Draw(renderer);
        this.Update(deltaTime);
    }
}