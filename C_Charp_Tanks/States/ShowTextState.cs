using System.Drawing;
using C_Charp_Tanks.Engine;
using C_Charp_Tanks.Engine.Renderer;

namespace C_Charp_Tanks.States;

public class ShowTextState: BaseGameState
{
    private string _text;
    
    private float _duration = 2f;
    private float _timeLeft;

    public ShowTextState(MapConfig mapConfig, char[,] layer) : base(mapConfig, layer)
    {
        
    }

    public string Text
    {
        get => _text;
        set => _text = value;
    }
    
    public override void Draw(BaseRenderer renderer)
    {
        int textHalfLength = Text.Length / 2;
        int textY = MapConfig.Height / 2;
        int textX = MapConfig.Width / 2;
        
        renderer.DrawString(Layer, textX, textY, Text);
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