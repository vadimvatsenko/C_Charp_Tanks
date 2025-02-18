using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.Engine;
using C_Charp_Tanks.Fabrics;
using C_Charp_Tanks.Venicals;
using C_Charp_Tanks.Venicals.Enemy;

namespace C_Charp_Tanks.Systems;

public class CollisionSystem : IUpdatable
{
    #region ItemsLists
    private List<Unit> _enemies = new List<Unit>();
    private List<Shell> _shells = new List<Shell>();
    private List<Block> _blocks = new List<Block>();
    private Unit _player;
    #endregion
    
    private FabricController _fabricController;
    private Random _random;

    public CollisionSystem(FabricController fabricController)
    {
        _fabricController = fabricController;
    }

    public void Update(double deltaTime)
    {
        _enemies = 
            _fabricController.UnitFabric.GetUnits().ToList().Where(u => u.UnitType == UnitType.Enemy).ToList();
        _player = 
            _fabricController.UnitFabric.GetUnits().ToList().Where(u => u.UnitType == UnitType.Player).FirstOrDefault();
        
        _blocks = _fabricController.BlocksFabric.GetBlocks().ToList();
        _shells = _fabricController.ShellsFabric.GetShells().ToList();
        
        foreach (var shell in _shells)
        {
            Vector2 newShellPos = shell.Position + shell.Direction;
            BoxCollider2D shellCollider = new BoxCollider2D(newShellPos, shell.BoxCollider2D.Size);
            
            foreach (var block in _blocks)
            {
                
                if (shellCollider.IsColliding(block.Collider) && block.Type == BlockType.Destructible)
                {
                    _fabricController.ShellsFabric.RemoveShell(shell);
                    block.GetDamage();
                }
                else if (shellCollider.IsColliding(block.Collider) && block.Type == BlockType.Indestructible)
                {
                    _fabricController.ShellsFabric.RemoveShell(shell);
                    //shell.Destroy();
                }
            }

            foreach (var unit in _enemies)
            {
                if (unit.Collider.IsColliding(shellCollider)) 
                {
                    //shell.Destroy();
                    unit.GetDamage(_random.Next(20, 40 ));
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