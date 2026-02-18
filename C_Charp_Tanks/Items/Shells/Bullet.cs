using C_Charp_Tanks.Engine.Renderer;

namespace C_Charp_Tanks.Items.Shells;

public class Bullet : Ammunition
{
    private double _timeElapsed = 0;
    public Bullet(Vector2 position, Vector2 dir) : base(position, dir)
    {
        View = Symbols.Bullet;
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
        renderer.DrawChar();
        renderer.SetPixel(Position.X, Position.Y, View, 4);
    }
}