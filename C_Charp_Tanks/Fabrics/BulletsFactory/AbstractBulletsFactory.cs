using System.Numerics;
using C_Charp_Tanks.Venicals;

namespace C_Charp_Tanks.Fabrics.BulletsFactory;

public abstract class AbstractShellsFactory
{
    protected FabricController _fabricController;
    protected List<Shell> _shells = new List<Shell>();
    
    public AbstractShellsFactory(FabricController fabricController)
    {
        _fabricController = fabricController;
    }
    public abstract void CreateShell(FabricController _fabricController, Vector2 position, Vector2 direction);
    public abstract void AddShell(Shell bullet); 
    public abstract void RemoveShell(Shell bullet);
    public abstract List<Shell> GetShells();
    public abstract void ClearShells();
}