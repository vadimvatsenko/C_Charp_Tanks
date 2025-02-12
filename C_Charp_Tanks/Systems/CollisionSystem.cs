using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.Venicals;

namespace C_Charp_Tanks.Systems;

public class CollisionSystem
{
    List<Unit> units = new List<Unit>();
    List<Bullet> bullets = new List<Bullet>();
    List<Block> blocks = new List<Block>();

    public CollisionSystem()
    {
        
    }
}