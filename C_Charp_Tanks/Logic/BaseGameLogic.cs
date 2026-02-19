using System.Drawing;
using C_Charp_Tanks.Engine;
using C_Charp_Tanks.Engine.Renderer;
using C_Charp_Tanks.States;

namespace C_Charp_Tanks.Logic;

public abstract class BaseGameLogic: IUpdatable
{
    protected BaseGameState? CurrentState { get; private set; }
    protected float Time {get; private set;}
    
    protected readonly MapConfig MapConfig;
    protected int ScreenWidth {get; private set;}
    protected int ScreenHeight {get; private set;}
    
    public abstract void Update(float deltaTime);

    public BaseGameLogic(MapConfig mapConfig)
    {
        MapConfig = mapConfig;
    }

    public void ChangeState(BaseGameState state)
    {
        CurrentState?.Reset();
        CurrentState = state;
    }

    public void DrawNewState(float deltaTime, BaseRenderer renderer)
    {
        Time += deltaTime;
        ScreenWidth = MapConfig.Width;
        ScreenHeight = MapConfig.Width;
        CurrentState?.Update(deltaTime);
        CurrentState?.Draw(renderer);
        this.Update(deltaTime);
    }

    public void Update(double deltaTime)
    {
        throw new NotImplementedException();
    }
}