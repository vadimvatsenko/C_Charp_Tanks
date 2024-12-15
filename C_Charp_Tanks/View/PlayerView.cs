using C_Charp_Tanks.Renderer;
using C_Charp_Tanks.Venicals;

namespace C_Charp_Tanks.Logics;

public class PlayerView : IUpdatable
{
    private Tank _tank;
    private GameData _gameData;
    
    private IRenderer _mainRenderer;
    private IRenderer _prevRenderer;
    
    public PlayerView(Tank tank, IRenderer renderer,  GameData gameData)
    {
        _mainRenderer = renderer;
        _prevRenderer = new ConsoleRenderer(Pallete.Colors);
        
        _tank = tank;
        _gameData = gameData;
    }

    public void Update(double deltaTime)
    {
        
        for (int x = 0; x < _gameData.Level.GetLength(1); x++)
        {
            for (int y = 0; y < _gameData.Level.GetLength(0); y++)
            {
                _mainRenderer.SetPixel(x, y, _gameData.Level[x,y], 7);
            }
        }
        
        _mainRenderer.SetPixel(_tank.position.X, _tank.position.Y, '\u25c9', 3);
        _mainRenderer.Render();
        
       // _prevRenderer = _mainRenderer;
    }
}