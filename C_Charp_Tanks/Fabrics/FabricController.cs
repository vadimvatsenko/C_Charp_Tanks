using C_Charp_Tanks.Fabrics.BlocksFactory;

namespace C_Charp_Tanks.Fabrics;
public class FabricController
{
    public UnitFabric _unitFabric { get; private set; }
    public BlocksFabric _blocksFabric { get; private set; }

    public FabricController(UnitFabric unitFabric, BlocksFabric blocksFabric)
    {
        _unitFabric = unitFabric;
        _blocksFabric = blocksFabric;
    }

    public void Initialize()
    {
        _unitFabric.CreateUnits();
        //_blocksFabric.CreateBlocks();
    }
    
}