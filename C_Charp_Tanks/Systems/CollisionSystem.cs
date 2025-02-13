using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.Engine;
using C_Charp_Tanks.Fabrics;
using C_Charp_Tanks.Venicals;

namespace C_Charp_Tanks.Systems;

public class CollisionSystem : IUpdatable
{
    private List<Unit> _units = new List<Unit>();
    private List<Bullet> _bullets = new List<Bullet>();
    private List<Block> _blocks = new List<Block>();
    
    private FabricController _fabricController;

    public CollisionSystem(FabricController fabricController)
    {
        _fabricController = fabricController;
    }

    public void Update(double deltaTime)
    {
        _units = _fabricController.UnitFabric.GetUnits();
        _blocks = _fabricController.BlocksFabric.GetBlocks();
        
        foreach (var unit in _units)
        {
            ResolveCollisions(unit);
        }
        
    }
    
    private void ResolveCollisions(Unit unit)
    {
        // Пробуем двигать юнита в его направлении движения
        Vector2 newPosition = unit.Position + unit.CurrentDirection;
        BoxCollider2D newCollider = new BoxCollider2D(newPosition, unit.Collider.Size);

        // Проверяем столкновение с блоками
        foreach (var block in _blocks)
        {
            if (newCollider.IsColliding(block.Collider))
            {
                //unit.Position = unit.Position; // Останавливаем юнита
                return; // Выход из метода, так как столкновение найдено
            }
        }
    }
}