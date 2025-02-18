using C_Charp_Tanks.Renderer;

namespace C_Charp_Tanks.Venicals;

public class Bullet : Shell
{
    private double _timeElapsed = 0;
    public Bullet(Vector2 position, Vector2 dir) : base(position, dir)
    {
        View = Symbols.Bullet;
        Speed = 5;
    }

    public override void Update(double deltaTime)
    {
        _timeElapsed += deltaTime * Speed;

        if (_timeElapsed >= 1)
        {
            _timeElapsed = 0; 
            Position += Direction; 
        }
    }

    public override void Render(IRenderer renderer)
    {
        renderer.SetPixel(Position.X, Position.Y, View, 4);
    }
    
    public override void Destroy()
    {
        //da_fabricController.ShellsFabric.RemoveShell(this);
    }
    
}