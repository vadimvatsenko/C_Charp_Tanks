using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.Engine;
using C_Charp_Tanks.Fabrics;
using C_Charp_Tanks.Venicals;
using C_Charp_Tanks.Venicals.Enemy;

namespace C_Charp_Tanks.Systems;

public class CollisionSystem : IUpdatable
{
    private List<Unit> _enemies = new List<Unit>();
    private List<Shell> _shells = new List<Shell>();
    private List<Block> _blocks = new List<Block>();
    private Unit _player;
    
    private FabricController _fabricController;
    private Random _random = new Random();

    public CollisionSystem(FabricController fabricController)
    {
        _fabricController = fabricController;
    }
    public void Update(double deltaTime)
    {
        
    }
}