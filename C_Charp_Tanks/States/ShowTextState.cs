using System.Drawing;

namespace C_Charp_Tanks.States;

public class ShowTextState: BaseGameState
{
    private string _text;
    
    private float _duration;
    private float _timeLeft;

    public string Text
    {
        get => _text;
        set => _text = value;
    }

    public ShowTextState(float duration) : this(string.Empty, duration)
    {
        
    }

    public ShowTextState(string text, float duration)
    {
        Text = text;
        _duration = duration;
        
          Reset();
    }
    
    public override void Draw(ConsoleRenderer consoleRenderer)
    {
        int textHalfLength = Text.Length / 2;
        int textY = consoleRenderer.height / 2;
        int textX = consoleRenderer.width / 2;
        consoleRenderer.DrawString(Text, textX, textY, ConsoleColor.DarkRed);
    }
    
    public override bool IsDone()
    {
        return _timeLeft <= 0f;
    }

    public override void Update(float deltaTime)
    {
        _timeLeft -= deltaTime;
    }

    public override void Reset()
    {
        _timeLeft = _duration;
    }
}