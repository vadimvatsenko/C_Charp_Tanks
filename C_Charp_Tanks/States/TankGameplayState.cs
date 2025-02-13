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
        _collisionSystem.Update(deltaTime);
        
        /*if (_fabricController.BulletsFabric.GetBullets().Count > 0)
        {
            foreach (var bullet in _fabricController.BulletsFabric.GetBullets())
            {
                bullet.Update(deltaTime);
            }
        }*/
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
        foreach (var block in _fabricController.BlocksFabric.GetBlocks())
        {
            block.Render(renderer);
        }
        
        foreach (var unit in _fabricController.UnitFabric.GetUnits())
        {
            unit.Render(renderer);
        }

        /*if (_fabricController.BulletsFabric.GetBullets().Count > 0 
            && _fabricController.BulletsFabric.GetBullets() != null)
        {
            foreach (var bullet in _fabricController.BulletsFabric.GetBullets())
            {
                bullet.Render(renderer);
            }
        }*/
        
        
        renderer.DrawString($"Score: {_score.ToString()}", FieldWidth / 2, 0, ConsoleColor.DarkBlue);
    }
}