using C_Charp_Tanks.Venicals;

namespace C_Charp_Tanks.Fabrics;

public abstract class AbstractUnitsFactory
{
    protected List<Unit> _units = new List<Unit>() ;
    public abstract void CreateUnits(int level);
    public abstract void AddUnit(Unit unit);
    public abstract void RemoveUnit(Unit unit);
    public abstract List<Unit> GetUnits();
    public abstract void ClearUnits();

}