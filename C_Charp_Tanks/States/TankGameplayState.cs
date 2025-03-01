using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.Engine;
using C_Charp_Tanks.Fabrics;
using C_Charp_Tanks.Systems;
using C_Charp_Tanks.Venicals;

namespace C_Charp_Tanks.States;
public class TankGameplayState : BaseGameState
{
    private FabricController _fabricController;
    private CollisionSystem _collisionSystem;
    private Random _random = new Random();

    #region GameObjects
    private List<Ammunition> _bullets = new List<Ammunition>();
    private List<Unit> _allUnits = new List<Unit>();
    private List<Unit> _enemies = new List<Unit>();
    private List<Block> _blocks = new List<Block>();
    private Unit? _player;
    #endregion
    
    private int _fieldWidth;
    private int _fieldHeight;

    private int _score = 0;
    private int _level = 1;
    
    public bool gameOver { get; private set; } = false;
    public bool hasWon { get; private set; } = false;
    
    #region Property
    public int FieldWidth
    {
        get => _fieldWidth;
        set => _fieldWidth = value;
    }

    public int FieldHeight
    {
        get => _fieldHeight;
        set => _fieldHeight = value;
    }

    public int Score
    {
        get => _score;
        set => _score = value;
    }
    
    public int Level
    {
        get => _level;
        set => _level = value;
    }
    #endregion

    public TankGameplayState(FabricController fabricController, CollisionSystem collisionSystem)
    {
        _collisionSystem = collisionSystem;
        _fabricController = fabricController;

        _fabricController.UnitFabric.OnItemsUpdated += UpdateUnits;
        _fabricController.BlocksFabric.OnItemsUpdated += UpdateBlocks;
        _fabricController.BulletsFabric.OnItemsUpdated += UpdateBullets;
        
        _collisionSystem.OnChangeScore += ChangeScore;
    }

    ~TankGameplayState()
    {
        _fabricController.UnitFabric.OnItemsUpdated -= UpdateUnits;
        _fabricController.BlocksFabric.OnItemsUpdated -= UpdateBlocks;
        _fabricController.BulletsFabric.OnItemsUpdated -= UpdateBullets;
        
        _collisionSystem.OnChangeScore -= ChangeScore;
    }
    
    public override bool IsDone()
    {
        return gameOver || hasWon;
    }

    public override void Update(float deltaTime)
    {
        _bullets.ForEach(s => s.Update(deltaTime));
        _enemies.ForEach(e => e.Update(deltaTime));
        _blocks.ForEach(b => b.Update(deltaTime));
        _player?.Update(deltaTime);
        
        _collisionSystem.Update(deltaTime);
        
        gameOver = _player == null;
        hasWon = _enemies.Count <= 0;
    }
    
    public override void Reset()
    {
        _fabricController.Clean();
        
        gameOver = false;
        hasWon = false;
    }
    
    public override void Draw(ConsoleRenderer renderer)
    {
        _blocks.ForEach(b => b.Render(renderer));
        _enemies.ForEach(e => e.Render(renderer));
        _bullets.ForEach(s => s.Render(renderer));
        _player?.Render(renderer);
        
        ConsoleColor healthColor = ConsoleColor.Cyan;
        if (_player != null)
        {
            healthColor = ChangeHealthColor(_player.Health);
        }
        
        renderer.DrawString
            ($"Score: {_score.ToString()}", FieldWidth / 2, 0, ConsoleColor.DarkGreen);
        renderer.DrawString
            ($"Health: {_player?.Health}%", FieldWidth / 4, 0, healthColor);
        renderer.DrawString
            ($"Enemies: {_enemies.Count()}", FieldWidth / 2 + FieldWidth / 4 , 0, ConsoleColor.DarkRed);
    }

    private ConsoleColor ChangeHealthColor(int health)
    {
        if(health >= 70 && health <= 100) 
            return ConsoleColor.DarkGreen;
        
        else if(health >= 50 && health < 70) 
            return ConsoleColor.DarkBlue;
        
        else 
            return ConsoleColor.DarkRed;
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

    private void ChangeScore()
    {
        Score += 100;
    }
}