// Этот класс:
// Управляет уникальной коллекцией объектов Unit (благодаря HashSet).
// Реализует интерфейс IEnumerable, чтобы обеспечить удобную итерацию.
// Может расширяться для добавления других методов управления коллекцией,
// например, поиска элементов или получения их количества.

using System.Collections;

namespace C_Charp_Tanks.Venicals;

public class Units : IEnumerable
{
    private HashSet<Unit> units = new();

    public void Add(Unit unit)
    {
        units.Add(unit);
    }

    public void Remove(Unit unit)
    {
        units.Remove(unit);
    }

    public IEnumerator GetEnumerator()
    {
        foreach (var unit in units)
        {
            yield return unit;
        }
    }
}