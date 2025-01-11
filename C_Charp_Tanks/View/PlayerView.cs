using C_Charp_Tanks.Renderer;
using C_Charp_Tanks.Venicals;

namespace C_Charp_Tanks.Logics;

public class PlayerView : IUpdatable
{
    /*private Tank _tank;
    
    private IRenderer _mainRenderer;
    private IRenderer _prevRenderer;
    
    public PlayerView(Tank tank, IRenderer renderer)
    {
        _mainRenderer = renderer;
        _prevRenderer = new ConsoleRenderer(Pallete.Colors);
        
        _tank = tank;
       
    }

    public void Update(double deltaTime)
    {
        
        for (int x = 0; x < GameData.Instance.Tank.GetLength(0); x++)
        {
            for (int y = 0; y < GameData.Instance.Tank.GetLength(1); y++)
            {
                _mainRenderer.SetPixel(x, y, GameData.Instance.Tank[x,y], 7);
            }
        }
        
        _mainRenderer.Render();
    }*/
    public void Update(double deltaTime)
    {
        //throw new NotImplementedException();
    }
}