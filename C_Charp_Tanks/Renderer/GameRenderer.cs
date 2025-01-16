namespace C_Charp_Tanks.Renderer;

public class GameRenderer : IUpdatable
{
    /*private char[,] _level;
    private IRenderer _renderer;
    private Tank _tank;

    public GameRenderer(IRenderer renderer, Tank tank, char[,] level)
    {
        _level = level;
        _renderer = renderer;
        _tank = tank;
    }

    public void DrawPoint(int w, int h, char symbol, byte colorIdx, int layer)
    {
        
    }

    public void DrawObject(int w, int h, char[,] symbols, byte colorIdx, int layer)
    {
        for (int x = 0; x < symbols.GetLength(0); x++)
        {
            for (int y = 0; y < symbols.GetLength(1); y++)
            {
                _renderer.SetPixel(x + w, y + h, symbols[x, y], colorIdx);
            }
        }
        
        _renderer.Render();
    }

    public void Update(double deltaTime)
    {

        DrawObject(0, 0, _level, 3, 3);
        DrawObject(_tank.Position.X, _tank.Position.Y, _tank.TankElement, _tank.Color, _tank.Layer);
    }*/

    public void Update(double deltaTime)
    {
        throw new NotImplementedException();
    }
}