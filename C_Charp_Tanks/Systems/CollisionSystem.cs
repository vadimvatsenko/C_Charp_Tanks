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
    
    public CollisionSystem(FabricController fabricController)
    {
        _fabricController = fabricController;
        
        _fabricController.UnitFabric.OnItemsUpdated += UpdateUnits;
        _fabricController.BlocksFabric.OnItemsUpdated += UpdateBlocks;
        _fabricController.BulletsFabric.OnItemsUpdated += UpdateBullets;
    }

    ~CollisionSystem()
    {
        _fabricController.UnitFabric.OnItemsUpdated -= UpdateUnits;
        _fabricController.BlocksFabric.OnItemsUpdated -= UpdateBlocks;
        _fabricController.BulletsFabric.OnItemsUpdated -= UpdateBullets;
    }
    
    public void Update(double deltaTime)
    {
        foreach (var bullet in _bullets)
        {
            Vector2 newBulltetPos = bullet.Position + bullet.Direction;
            BoxCollider2D bulletColl = new BoxCollider2D(newBulltetPos, bullet.Collider.Size);
            
            foreach (var block in _blocks)
            {
                if (bulletColl.IsColliding(block.Collider) && block.Type == BlockType.Destructible)
                {
                    _fabricController.BulletsFabric.RemoveItem(bullet);
                    block.GetDamage();
                }

                if (bulletColl.IsColliding(block.Collider) && block.Type == BlockType.Indestructible)
                {
                    _fabricController.BulletsFabric.RemoveItem(bullet);
                }
                
            }
            foreach (var u in _allUnits)
            {
                if (bulletColl.IsColliding(u.Collider))
                {
                    _fabricController.BulletsFabric.RemoveItem(bullet);
                    
                    if (u.UnitType == UnitType.Player) u.GetDamage(_random.Next(0, 51));
                    if (u.UnitType == UnitType.Enemy) u.GetDamage(_random.Next(0, 101));
                    
                    if (u.Health <= 0)
                    {
                        _fabricController.UnitFabric.RemoveItem(u);
                        if (u.UnitType == UnitType.Enemy)
                        {
                            //Score += 100;
                            OnChangeScore?.Invoke();
                        }
                        
                    }
                }
            }

            foreach (var bul in _bullets.Where(b => b != bullet))
            {
                if (bulletColl.IsColliding(bul.Collider))
                {
                    _fabricController.BulletsFabric.RemoveItem(bul);
                    _fabricController.BulletsFabric.RemoveItem(bullet);
                }
            }
        }
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