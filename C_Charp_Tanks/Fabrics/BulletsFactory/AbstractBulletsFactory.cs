using System.Numerics;
using C_Charp_Tanks.Venicals;

namespace C_Charp_Tanks.Fabrics.BulletsFactory;

public abstract class AbstractBulletsFactory
{
    protected FabricController _fabricController;
    
    public AbstractBulletsFactory(FabricController fabricController)
    {
        _fabricController = fabricController;
    }
    protected List<Bullet> _bullets;
    public abstract void CreateBullet(Vector2 position, Vector2 direction);
    public abstract void AddBullet(Bullet bullet);
    public abstract void RemoveBullet(Bullet bullet);
    public abstract List<Bullet> GetBullets();
}