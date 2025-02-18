using C_Charp_Tanks.Renderer;

namespace C_Charp_Tanks.Blocks;

public class WaterBlock : Block
{
    private double _timeElapsed = 0;
    private bool _isFirstState = true;
    public WaterBlock(BlockType type, char symbol, Vector2 position) : base(type, symbol, position)
    {
        Color = 1;
    }

    public override void Update(double deltaTime)
    {
        _timeElapsed += deltaTime;

        if(_timeElapsed >= 1)
        {
            _timeElapsed = 0;
            SwapWave();
        }
    }

    private void SwapWave()
    {
        _isFirstState = !_isFirstState;
        Symbol = _isFirstState ? Symbols.WaterStateOne : Symbols.WaterStateTwo;
        FillBlock();
    }
}