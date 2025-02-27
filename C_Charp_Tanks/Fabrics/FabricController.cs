using C_Charp_Tanks.Fabrics.BlocksFactory;
using C_Charp_Tanks.Fabrics.BulletsFactory;

namespace C_Charp_Tanks.Fabrics;
public class FabricController
{
    public UnitFabric UnitFabric { get; private set; }
    public BlocksFabric BlocksFabric { get; private set; }
    public BulletsFabric BulletsFabric { get; private set; }

    public FabricController(UnitFabric unitFabric, BlocksFabric blocksFabric)
    {
        UnitFabric = unitFabric;
        BlocksFabric = blocksFabric;
        BulletsFabric = new BulletsFabric(this);
    }

    public void Initialize(int level)
    {
        UnitFabric.SetLevel(level);
        UnitFabric.CreateItem();
        BlocksFabric.CreateItem();
    }

    public void CleanUp()
    {
        UnitFabric.CreateItem();
        BlocksFabric.CreateItem();
    }
}