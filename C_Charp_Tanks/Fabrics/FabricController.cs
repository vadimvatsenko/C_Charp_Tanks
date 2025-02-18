using C_Charp_Tanks.Fabrics.BlocksFactory;
using C_Charp_Tanks.Fabrics.BulletsFactory;

namespace C_Charp_Tanks.Fabrics;
public class FabricController
{
    public UnitFabric UnitFabric { get; private set; }
    public BlocksFabric BlocksFabric { get; private set; }
    public ShellFabric ShellsFabric { get; private set; }

    public FabricController(UnitFabric unitFabric, BlocksFabric blocksFabric)
    {
        UnitFabric = unitFabric;
        BlocksFabric = blocksFabric;
        ShellsFabric = new ShellFabric(this);
    }

    public void Initialize(int level)
    {
        UnitFabric.CreateUnits(level);
        BlocksFabric.CreateBlocks();
    }

    public void CleanUp()
    {
        UnitFabric.ClearUnits();
        BlocksFabric.ClearBlocks();
    }
    
}