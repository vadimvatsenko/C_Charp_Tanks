namespace C_Charp_Tanks.Renderer;

public class GameRenderer : IUpdatable
{
    private IRenderer _renderer;

    public GameRenderer(IRenderer renderer)
    {
        _renderer = renderer;
    }

    public void DrawPoint(int w, int h, char synbol, byte colorIdx, int layer)
    {
        
    }

    public void DrawObject(int w, int h, char[,] synbol, byte colorIdx, int layer)
    {
        
    }
    
    public void Update(double deltaTime)
    {
        
    }
}