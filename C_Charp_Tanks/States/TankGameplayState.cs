using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.Engine;
using C_Charp_Tanks.Fabrics;
using C_Charp_Tanks.Systems;
using C_Charp_Tanks.Venicals;
using C_Charp_Tanks.Venicals.Enemy;

namespace C_Charp_Tanks.States;

public class TankGameplayState : BaseGameState
{
    private FabricController _fabricController;
    private CollisionSystem _collisionSystem;
    private Random _random = new Random();

    #region GameObjects
    private List<Shell> _shells = new List<Shell>();
    private List<Unit> _allUnits = new List<Unit>();
    private List<Unit> _enemies = new List<Unit>();
    private List<Block> _blocks = new List<Block>();
    private List<Unit> _players = new List<Unit>();
    #endregion
    
    private int _fieldWidth;
    private int _fieldHeight;

    private int _score = 0;
    private int _level = 1;
    
    public bool gameOver { get; private set; }
    public bool hasWon { get; private set; } = false;
    
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

    public TankGameplayState(FabricController fabricController, CollisionSystem collisionSystem)
    {
        _collisionSystem = collisionSystem;
        _fabricController = fabricController;

        _allUnits = _fabricController.UnitFabric.GetItem();
        _players = _allUnits.FindAll(u => u.UnitType == UnitType.Player);
        _enemies = _allUnits.FindAll(u => u.UnitType == UnitType.Enemy);
        _blocks = 
            _fabricController.BlocksFabric.GetItem().Where(b => b.Type != BlockType.Indestructible).ToList();
        
    }
    
    public override bool IsDone()
    {
        return gameOver || hasWon;
    }

    public override void Update(float deltaTime)
    {
        _shells.ForEach(s => s.Update(deltaTime));
        _enemies.ForEach(e => e.Update(deltaTime));
        _blocks.ForEach(b => b.Update(deltaTime));
        _players.ForEach(p => p.Update(deltaTime));
        
        CheckCollisions(_blocks, _shells, _enemies, _players[0]);
        
        //_collisionSystem.Update(deltaTime);
        
        gameOver = _players[0].Health <= 0;
        hasWon = _enemies.Count <= 0;
    }

    private void CheckCollisions(List<Block> blocks, List<Shell> shells, List<Unit> enemies, Unit player)
    {
        foreach (var shell in shells)
        {
            Vector2 newShellPos = shell.Position + shell.Direction;
            BoxCollider2D shellCollider = new BoxCollider2D(newShellPos, shell.Collider.Size);
            
            foreach (var block in blocks)
            {
                if (shellCollider.IsColliding(block.Collider) && block.Type == BlockType.Destructible)
                {
                    shell.Destroy(_fabricController);
                    block.GetDamage();
                }

                if (shellCollider.IsColliding(block.Collider) && block.Type == BlockType.Indestructible)
                {
                    shell.Destroy(_fabricController);
                }
                
            }
            foreach (var enemy in enemies)
            {
                if (shellCollider.IsColliding(enemy.Collider))
                {
                    _fabricController.ShellsFabric.RemoveShell(shell);
                    enemy.GetDamage(_random.Next(0, 101));
                    if (enemy.Health <= 0)
                    {
                        enemy.Destroy();
                        Score += 100;
                    }
                }
            }
        }
    }

    public override void Reset()
    {
        gameOver = false;
        hasWon = false;
        int middleX = FieldWidth / 2; 
        int middleY = FieldHeight / 2; 
        
        _fabricController.ShellsFabric.GetShells().Clear();
        _fabricController.UnitFabric.GetItem().Clear();
        _fabricController.BlocksFabric.GetItem().Clear();
    }
    
    public override void Draw(ConsoleRenderer renderer)
    {
        _blocks.ForEach(b => b.Render(renderer));
        _enemies.ForEach(e => e.Render(renderer));
        _shells.ForEach(s => s.Render(renderer));
        _players.ForEach(p => p.Render(renderer));
        
        renderer.DrawString
            ($"Score: {_score.ToString()}", FieldWidth / 2, 0, ConsoleColor.DarkBlue);
        renderer.DrawString
            ($"Health: {_players[0].Health}%", FieldWidth / 2 - 30 , 0, ConsoleColor.DarkBlue);
        renderer.DrawString
            ($"Enemies: {_enemies.Count()}", FieldWidth / 2 + 23 , 0, ConsoleColor.DarkBlue);
    }
}