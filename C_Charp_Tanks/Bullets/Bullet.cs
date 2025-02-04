using C_Charp_Tanks.Venicals;namespace C_Charp_Tanks.Bullets;

public abstract class Bullet : IUpdatable
{
    public char BulletSymbol { get; protected set; }
    public Vector2 BulletPosition { get; protected set; }
    public Direction BulletDirection {get; protected set;}
    
    public void Update(double deltaTime)
    {
         
    }
}