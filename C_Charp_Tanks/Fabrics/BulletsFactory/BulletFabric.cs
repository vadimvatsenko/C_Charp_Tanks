using C_Charp_Tanks.Venicals;

namespace C_Charp_Tanks.Fabrics.BulletsFactory;

public class BulletFabric : AbstractBulletsFactory
{
    public BulletFabric(FabricController fabricController) : base(fabricController)
    {
    }

    public override void AddBullet(Bullet bullet) =>  _bullets.Add(bullet);
    public override void RemoveBullet(Bullet bullet) => _bullets.Remove(bullet);

    public override List<Bullet> GetBullets() => _bullets;
    
    public override void CreateBullet(Vector2 position, Vector2 direction)
    {
        Bullet bullet = new Bullet(position, _fabricController, direction);
        if (bullet == null)
        {
            throw new Exception("Bullet is null");
            
        }
        _bullets.Add(bullet);
    }
}