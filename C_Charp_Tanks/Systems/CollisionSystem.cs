using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.Engine;
using C_Charp_Tanks.Fabrics;
using C_Charp_Tanks.Venicals;

namespace C_Charp_Tanks.Systems;

public class CollisionSystem : IUpdatable
{
    private List<Unit> _units = new List<Unit>();
    private List<Shell> _shells = new List<Shell>();
    private List<Block> _blocks = new List<Block>();
    
    private Unit _player;
    
    private FabricController _fabricController;
    private Random _random;

    public CollisionSystem(FabricController fabricController)
    {
        _fabricController = fabricController;
    }

    public void Update(double deltaTime)
    {
        _units = _fabricController.UnitFabric.GetUnits().ToList();
        _blocks = _fabricController.BlocksFabric.GetBlocks().ToList();
        _shells = _fabricController.ShellsFabric.GetShells().ToList();
        
        _player = _fabricController.UnitFabric.GetUnits().ToList().Where(u => u is Player).First();

        foreach (var shell in _shells)
        {
            foreach (var block in _blocks)
            {
                Vector2 newShellPos = shell.Position + shell.Direction;
                BoxCollider2D shellCollider = new BoxCollider2D(newShellPos, shell.BoxCollider2D.Size);
                if (shellCollider.IsColliding(block.Collider) && block.Type == BlockType.Destructible)
                {
                    shell.Destroy();
                    block.GetDamage();
                }
                else if (shellCollider.IsColliding(block.Collider) && block.Type == BlockType.Indestructible)
                {
                    shell.Destroy();
                }
            }

            foreach (var unit in _units)
            {
                Vector2 newShellPos = shell.Position + shell.Direction;
                BoxCollider2D shellCollider = new BoxCollider2D(newShellPos, shell.BoxCollider2D.Size);
                if (shellCollider.IsColliding(unit.Collider))
                {
                    shell.Destroy();
                    //unit.GetDamage(_random.Next(0, 101));
                }
            }
        }
    }

    /*public bool TryToMove()
    {
        
    }*/
    
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