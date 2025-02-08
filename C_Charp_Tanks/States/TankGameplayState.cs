using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.Venicals;
using C_Charp_Tanks.Venicals.Enemy;

namespace C_Charp_Tanks.States;

public class TankGameplayState : BaseGameState
{
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
    
    public override bool IsDone()
    {
        return gameOver || hasWon;
    }

    public override void Update(float deltaTime)
    {
        var bullets = BulletObjects.Instance.GetObjects().ToList(); // нужно локальную копию списка, так как нельзя удалять на горячюю из списка
        var units = UnitObjects.Instance.GetObjects().ToList();
        var blocks = BlockObjects.Instance.GetObjects().ToList();
        
        for (int i = bullets.Count - 1; i >= 0; i--) // Идём с конца списка
        {
            bullets[i].Update(deltaTime);
    
            if (bullets[i].IsDestroyed) // Если пуля уничтожена
            {
                BulletObjects.Instance.RemoveObject(bullets[i]); // Удаляем пулю
            }
        }
        
        for (int i = blocks.Count - 1; i >= 0; i--)
        {
            blocks[i].Update(deltaTime);
    
            if (blocks[i].IsDestroyed) 
            {
                BlockObjects.Instance.RemoveObject(blocks[i]); 
            }
        }
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
        foreach (var block in BlockObjects.Instance.GetObjects())
        {
            block.Render(renderer);
        }

        foreach (var unit in UnitObjects.Instance.GetObjects())
        {
            unit.Render(renderer);
        }
        
        if (BulletObjects.Instance.GetObjects().Any())
        {
            foreach (var bullet in BulletObjects.Instance.GetObjects())
            {
                bullet.Render(renderer);
            }
        }
        
        
        renderer.DrawString($"Score: {_score.ToString()}", FieldWidth / 2, 0, ConsoleColor.DarkBlue);
    }
}