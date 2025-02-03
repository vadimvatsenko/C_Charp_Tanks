using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.Venicals;

namespace C_Charp_Tanks.States;

public class TankGameplayState : BaseGameState
{
    private int _fieldWidth;
    private int _fieldHeight;

    private int _score = 0;
    private int _lives = 3;
    private int _level = 1;
    
    private Player _player;
    
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

    public int Lives
    {
        get => _lives;
        set => _lives = value;
    }

    public int Level
    {
        get => _level;
        set => _level = value;
    }
    
    
    public TankGameplayState(Player player)
    {
        _player = player;
    }
    public override bool IsDone()
    {
        return gameOver || hasWon;
    }

    public override void Update(float deltaTime)
    {
        _player.Update(deltaTime);
    }

    public override void Reset()
    {
        gameOver = false;
        hasWon = false;
        //_bodyList.Clear();
        int middleX = FieldWidth / 2; 
        int middleY = FieldHeight / 2; 

        //_currentDir = SnakeDir.Right;
        //_bodyList.Add(new Cell(middleX, middleY)); 
        //_timeToMove = 0;
        //_apple = new Cell(middleX + 3, middleY + 3);
    }

    public override void Draw(ConsoleRenderer renderer)
    {
        foreach (Block block in BlocksController.Blocks)
        {
            block.RendererBlocks(renderer);
        }
        
        _player.RenderPlayer(renderer);
        
        renderer.DrawString($"Score: {_score.ToString()}", FieldWidth / 2, 0, ConsoleColor.DarkBlue);
        
    }
}