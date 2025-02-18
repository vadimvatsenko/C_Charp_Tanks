using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.Fabrics;
using C_Charp_Tanks.Systems;
using C_Charp_Tanks.Venicals;
using C_Charp_Tanks.Venicals.Enemy;

namespace C_Charp_Tanks.States;

public class TankGameplayState : BaseGameState
{
    private FabricController _fabricController;
    private CollisionSystem _collisionSystem;
    
    private int _fieldWidth;
    private int _fieldHeight;

    private int _score = 0;
    private int _level = 1;

    private int _timeToMove = 0;
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
    }
    
    public override bool IsDone()
    {
        return gameOver || hasWon;
    }

    public override void Update(float deltaTime)
    {
        // копия, нельзя удалять из списка, во время цикла
        var shells = _fabricController.ShellsFabric.GetShells().ToList();
        var allUnits = _fabricController.UnitFabric.GetUnits().ToList();
        var blocks = _fabricController.BlocksFabric.GetBlocks().ToList();
        
        var enemies = _fabricController.UnitFabric.GetUnits().Where(u => u is Enemy).ToList();
        var player = _fabricController.UnitFabric.GetUnits().Where(u => u is Player).First();
        
        _collisionSystem.Update(deltaTime);

        foreach (var shell in shells) shell.Update(deltaTime);
        
        foreach (var unit in allUnits) unit.Update(deltaTime);
        
        foreach (var block in blocks) block.Update(deltaTime);
        
        gameOver = player.Health <= 0;
        hasWon = enemies.Count <= 0;
        
        _collisionSystem.Update(deltaTime);
    }

    public override void Reset()
    {
        gameOver = false;
        hasWon = false;
        int middleX = FieldWidth / 2; 
        int middleY = FieldHeight / 2; 
        
        _timeToMove = 0;
    }
    
    public override void Draw(ConsoleRenderer renderer)
    {
        var player 
            = _fabricController.UnitFabric.GetUnits().Where(u => u.UnitType == UnitType.Player).FirstOrDefault();
        var enemies 
            = _fabricController.UnitFabric.GetUnits().ToList().Where(u => u.UnitType == UnitType.Enemy);
        var allUnits = _fabricController.UnitFabric.GetUnits().ToList();
        
        var shells = _fabricController.ShellsFabric.GetShells().ToList();
        var blocks = _fabricController.BlocksFabric.GetBlocks().ToList();
        
        
        foreach (var block in blocks)
        {
            block.Render(renderer);
        }
        
        foreach (var unit in allUnits)
        {
            unit.Render(renderer);
        }
        
        foreach (var shell in shells)
        {
            shell.Render(renderer);
        }
        
        renderer.DrawString($"Score: {_score.ToString()}", FieldWidth / 2, 0, ConsoleColor.DarkBlue);
        renderer.DrawString($"Health: {player.Health}%", FieldWidth / 2 - 30 , 0, ConsoleColor.DarkBlue);
        renderer.DrawString($"Enemies: {enemies.Count()}", FieldWidth / 2 + 23 , 0, ConsoleColor.DarkBlue);
    }
}