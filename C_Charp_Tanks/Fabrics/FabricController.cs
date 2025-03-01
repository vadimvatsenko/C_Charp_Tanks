using C_Charp_Tanks.Fabrics.BlocksFactory;
using C_Charp_Tanks.Fabrics.BulletsFactory;

namespace C_Charp_Tanks.Fabrics;
public class FabricController
{
    public UnitFabric UnitFabric { get; private set; }
    public BlocksFabric BlocksFabric { get; private set; }
    public BulletsFabric BulletsFabric { get; private set; }
    
    public List<Vector2> EmptyPositions { get; private set; } = new List<Vector2>();
    
    public FabricController(UnitFabric unitFabric, BlocksFabric blocksFabric, BulletsFabric bulletsFabric)
    {
        BlocksFabric = blocksFabric;
        UnitFabric = unitFabric;
        BulletsFabric = bulletsFabric;
    }

    public void Initialize(int level)
    {
        UnitFabric.SetLevel(level);
        UnitFabric.CreateUnits();
    }

    public void Clean()
    {
        UnitFabric.ClearItems();
        BlocksFabric.ClearItems();
        BulletsFabric.ClearItems();
    }

    public void AddEmptyPosition(Vector2 position)
    {
        EmptyPositions.Add(position);
    }
}