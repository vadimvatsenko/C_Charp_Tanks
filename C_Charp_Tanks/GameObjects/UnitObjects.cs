using System.Collections;
using C_Charp_Tanks.Venicals;

namespace C_Charp_Tanks.Blocks;

public class UnitObjects : GameObjects<Unit>
{
    public static UnitObjects Instance { get; } = new UnitObjects();

    private UnitObjects()
    {
        
    }
}