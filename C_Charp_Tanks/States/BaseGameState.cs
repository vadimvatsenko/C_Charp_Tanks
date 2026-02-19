using C_Charp_Tanks.Engine;
using C_Charp_Tanks.Engine.Renderer;

namespace C_Charp_Tanks.States;

public abstract class BaseGameState
{
    protected MapConfig MapConfig;
    protected char[,] Layer;

    protected BaseGameState(MapConfig mapConfig, char[,] layer)
    {
        MapConfig = mapConfig;
        Layer = layer;
    }

    public abstract bool IsDone();
    public abstract void Update(float deltaTime);
    public abstract void Reset();
    public abstract void Draw(BaseRenderer renderer);
}