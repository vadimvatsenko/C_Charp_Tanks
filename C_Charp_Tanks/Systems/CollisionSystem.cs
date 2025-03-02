using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.Engine;
using C_Charp_Tanks.Fabrics;
using C_Charp_Tanks.Venicals;
using C_Charp_Tanks.Venicals.Enemy;

namespace C_Charp_Tanks.Systems;

public class CollisionSystem : IUpdatable
{
    private List<Unit> _allUnits = new List<Unit>();
    private List<Unit> _enemies = new List<Unit>();
    private Unit _player;
    
    private List<Ammunition> _bullets = new List<Ammunition>();
    private List<Block> _blocks = new List<Block>();
    
    private FabricController _fabricController;
    private Random _random = new Random();
    public event Action OnChangeScore;
    
    ~CollisionSystem()
    {
        _fabricController.UnitFabric.OnItemsUpdated -= UpdateUnits;
        _fabricController.BlocksFabric.OnItemsUpdated -= UpdateBlocks;
        _fabricController.BulletsFabric.OnItemsUpdated -= UpdateBullets;
    }

    public void SetFabricController(FabricController fabricController)
    {
        _fabricController = fabricController;
        
        _fabricController.UnitFabric.OnItemsUpdated += UpdateUnits;
        _fabricController.BlocksFabric.OnItemsUpdated += UpdateBlocks;
        _fabricController.BulletsFabric.OnItemsUpdated += UpdateBullets;
    }
    
    public void Update(double deltaTime)
    {
        BulletsCollision();
    }

    private void BulletsCollision()
    {
        
        if (_bullets.Count > 0)
        {
            var bulletsToRemove = new HashSet<Ammunition>();
            var unitsToRemove = new HashSet<Unit>();
            var blocksToRemove = new HashSet<Block>();

            foreach (var bullet in _bullets)
            {
                Vector2 newBulletPos = bullet.Position + bullet.Direction;
                BoxCollider2D bulletColl = new BoxCollider2D(newBulletPos, bullet.Collider.Size);

                // Проверяем столкновение с блоками
                
                foreach (var block in _blocks)
                {
                    if (bulletColl.IsColliding(block.Collider) && block.Type != BlockType.Water)
                    {
                        if (block.Type == BlockType.Destructible)
                        {
                            DestructibleBlock? destructibleBlock = block as DestructibleBlock;
                            destructibleBlock?.GetDamage();
                            if (destructibleBlock.Lives <= 0)
                            {
                                blocksToRemove.Add(block);
                            }
                            
                        }

                        bulletsToRemove.Add(bullet);
                        break; // Пуля не может пройти дальше, выходим из проверки блоков
                    }
                }

                // Проверяем столкновение с юнитами (игроком и врагами)
                foreach (var unit in _allUnits)
                {
                    if (bulletColl.IsColliding(unit.Collider))
                    {
                        unit.GetDamage(unit.UnitType == UnitType.Player ? _random.Next(0, 51) : _random.Next(0, 101));

                        if (unit.Health <= 0)
                        {
                            unitsToRemove.Add(unit);

                            if (unit.UnitType == UnitType.Enemy)
                            {
                                OnChangeScore?.Invoke();
                            }
                        }

                        bulletsToRemove.Add(bullet);
                        break;
                    }
                }

                // Проверяем столкновение с другими пулями
                /*foreach (var otherBullet in _bullets.Where(b => b != bullet))
                {
                    if (bulletColl.IsColliding(otherBullet.Collider))
                    {
                        bulletsToRemove.Add(bullet);
                        bulletsToRemove.Add(otherBullet);
                        break;
                    }
                }
                */


            }

            // Удаляем все пули, которые столкнулись
            foreach (var bullet in bulletsToRemove)
            {
                _fabricController.BulletsFabric.RemoveItem(bullet);
            }

            foreach (var u in unitsToRemove)
            {
                _fabricController.UnitFabric.RemoveItem(u);
            }

            foreach (var b in blocksToRemove)
            {
                _fabricController.BlocksFabric.RemoveItem(b);
            }
            
            
        }

    }

    public bool IsUnwalkable(int x, int y, Unit currentUnit)
    {
        Vector2 pos = new Vector2(x, y);
        BoxCollider2D tempCollider = new BoxCollider2D(pos, Vector2.Tree);
        
        bool isBlock = _blocks.Any(b => b.Collider.IsColliding(tempCollider));
        return !isBlock;
    }
    
    public bool IsUnwalkable(int x, int y, Vector2 direction)
    {
        
        Vector2 nextPos = new Vector2(x, y) + direction;
        BoxCollider2D tempCollider = new BoxCollider2D(nextPos, Vector2.Tree);
        
        bool isBlock = _blocks.Any(b => b.Collider.IsColliding(tempCollider));
        return !isBlock;
    }
    
    private void UpdateBullets()
    {
        _bullets = _fabricController.BulletsFabric.GetItems().ToList();
    }

    private void UpdateBlocks()
    {
        _blocks = _fabricController.BlocksFabric.GetItems().ToList();
    }

    private void UpdateUnits()
    {
        _allUnits = _fabricController.UnitFabric.GetItems().ToList();
        _enemies = _allUnits.FindAll(u => u.UnitType == UnitType.Enemy);
        _player = _allUnits.Find(u => u.UnitType == UnitType.Player);
    }
}