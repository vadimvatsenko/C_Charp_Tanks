using C_Charp_Tanks.Venicals;

namespace C_Charp_Tanks.Blocks;

public class BulletObjects : GameObjects<Bullet>
{
    public static BulletObjects Instance { get;} = new();

    private BulletObjects()
    {
        
    }
}