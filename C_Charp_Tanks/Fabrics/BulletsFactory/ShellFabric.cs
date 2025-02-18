using C_Charp_Tanks.Venicals;

namespace C_Charp_Tanks.Fabrics.BulletsFactory;

public class ShellFabric : AbstractShellsFactory
{
    public ShellFabric(FabricController fabricController) : base(fabricController)
    {
    }
    
    public override void CreateShell(FabricController fabricController, Vector2 position, Vector2 direction)
    {
        Shell shell = new Bullet(position, direction);
        _shells.Add(shell);
    }

    public override void AddShell(Shell bullet) => _shells.Add(bullet);
    
    public override void RemoveShell(Shell bullet) => _shells.Remove(bullet);
    
    public override List<Shell> GetShells() => _shells;
    public override void ClearShells() => _shells.Clear();
  
}