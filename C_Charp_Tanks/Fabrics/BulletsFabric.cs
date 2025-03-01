using C_Charp_Tanks.Venicals;

namespace C_Charp_Tanks.Fabrics.BulletsFactory;

public class BulletsFabric : AbstractFabric<Ammunition>
{
    
    public void CreateBullet(Vector2 position, Vector2 direction)
    {
        Bullet bullet = new Bullet(position, direction);
        AddItem(bullet);
    }
}