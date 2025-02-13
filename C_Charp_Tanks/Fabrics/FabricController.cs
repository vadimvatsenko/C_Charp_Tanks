using C_Charp_Tanks.Fabrics.BlocksFactory;
using C_Charp_Tanks.Fabrics.BulletsFactory;

namespace C_Charp_Tanks.Fabrics;
public class FabricController
{
    public UnitFabric UnitFabric { get; private set; }
    public BlocksFabric BlocksFabric { get; private set; }
    public BulletFabric BulletsFabric { get; private set; }

    public FabricController(UnitFabric unitFabric, BlocksFabric blocksFabric)
    {
        UnitFabric = unitFabric;
        BlocksFabric = blocksFabric;
        BulletsFabric = new BulletFabric(this);
    }

    public void Initialize()
    {
        UnitFabric.CreateUnits();
        BlocksFabric.CreateBlocks();
    }
    
}