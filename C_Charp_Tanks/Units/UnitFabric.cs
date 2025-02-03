using C_Charp_Tanks.Renderer;

namespace C_Charp_Tanks.Venicals;

public class UnitFabric
{
    private IRenderer _renderer;
    private IConsoleInput _consoleInput;

    public UnitFabric(IRenderer renderer, IConsoleInput consoleInput)
    {
        _renderer = renderer;
        _consoleInput = consoleInput;
    }

    /*public void CreateUnit(UnitConfig unitConfig)
    {
        switch (unitConfig.UnitType)
        {
            case UnitType.Player:
                Player player = new Player(unitConfig.Position, _renderer, _consoleInput);
                break;
        }
    }*/
}