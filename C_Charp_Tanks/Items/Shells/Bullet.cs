using C_Charp_Tanks.Engine.Renderer;

namespace C_Charp_Tanks.Items.Shells;

public class Bullet : Ammunition
{
    private double _timeElapsed = 0;
    private readonly char[,] _bulletLayer;
    public Bullet(Vector2 position, Vector2 dir, char[,] bulletLayer) : base(position, dir)
    {
        View = Symbols.Bullet;
        _bulletLayer = bulletLayer;
        Speed = 5;
    }

    public override void Update(double deltaTime)
    {
        Collider.Position = Position;
        _timeElapsed += deltaTime * Speed;

        if (_timeElapsed >= 1)
        {
            _timeElapsed = 0; 
            Position += Direction; 
        }
    }

    public override void Render(BaseRenderer renderer)
    {
        renderer.DrawChar(_bulletLayer, Position.X, Position.Y, View);
    }
}