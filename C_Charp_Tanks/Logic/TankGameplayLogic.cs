using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.Fabrics;
using C_Charp_Tanks.MazeGenerator;
using C_Charp_Tanks.States;
using C_Charp_Tanks.Venicals;

namespace C_Charp_Tanks.Logic;

public class TankGameplayLogic : BaseGameLogic
{
    private TankGameplayState _tankGameplayState;
    private ShowTextState _showTextState = new ShowTextState(2f);
    private MazeCreator _mazeCreator;
    private FabricController _fabricController;
    private bool _newGamePending = false;
    private int _currentLevel = 0;
    
    public TankGameplayLogic(TankGameplayState tankGameplayState, MazeCreator mazeCreator, FabricController fabricController)
    {
        _mazeCreator = mazeCreator;
        _tankGameplayState = tankGameplayState;
        _fabricController = fabricController;
        
        // Temp
        _mazeCreator.Initialize();
        _fabricController.Initialize();
    }
    
    public void GotoGamePlay()
    {
        
        _tankGameplayState.Level = _currentLevel;
        _tankGameplayState.FieldWidth = this.ScreenWidth;
        _tankGameplayState.FieldHeight = this.ScreenHeight;
        ChangeState(_tankGameplayState);
        _tankGameplayState.Reset();
    }

    public void GotoGameOver()
    {
        _currentLevel = 0;
        _tankGameplayState.Score = 0;
        _newGamePending = true;
        _showTextState.Text = "GAME OVER";
        ChangeState(_showTextState);
    }

    public void GotoNextLevel()
    {
        _currentLevel++;
        _newGamePending = false;
        _showTextState.Text = $"Level: {_currentLevel}";
        ChangeState(_showTextState);
    }
    public override void Update(float deltaTime)
    {
        if(CurrentState != null && !CurrentState.IsDone()) return;

        if ((CurrentState == null || CurrentState == _tankGameplayState) && !_tankGameplayState.gameOver)
        {
            GotoNextLevel();
        }
        else if (CurrentState == _tankGameplayState && _tankGameplayState.gameOver)
        {
            GotoGameOver();
        }
        else if (CurrentState != _tankGameplayState && _newGamePending)
        {
            GotoNextLevel();
        }
        else if(CurrentState != _tankGameplayState && !_newGamePending)
        {
            GotoGamePlay();
        }
    }
}