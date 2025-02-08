namespace C_Charp_Tanks;

public class PlayerData
{
    private static PlayerData? _instance;

    private PlayerData()
    {
        
    }

    public static PlayerData? Instance
    {
        get 
        {
            if (_instance == null)
            {
                _instance = new PlayerData();
            }
            return _instance;
        }
    }
    
    private readonly char[,] _tankUpView = new char[,]
    {
        { Symbols.TankTracks, Symbols.TankTracks, Symbols.TankTracks },
        { Symbols.TankMuzzle, Symbols.TankTurret, Symbols.TankBack },
        { Symbols.TankTracks, Symbols.TankTracks, Symbols.TankTracks },
    };
    
    private readonly char[,] _tankDownView = new char[,]
    {
        { Symbols.TankTracks, Symbols.TankTracks, Symbols.TankTracks },
        { Symbols.TankBack, Symbols.TankTurret,  Symbols.TankMuzzle },
        { Symbols.TankTracks, Symbols.TankTracks, Symbols.TankTracks },
    };
    
    
    private readonly char[,] _tankLeftView = new char[,]
    {
        { Symbols.TankTracks, Symbols.TankBack, Symbols.TankTracks },
        { Symbols.TankTracks, Symbols.TankTurret, Symbols.TankTracks },
        { Symbols.TankTracks, Symbols.TankMuzzle, Symbols.TankTracks },
    };
    
    private readonly char[,] _tankRightView = new char[,]
    {
        { Symbols.TankTracks, Symbols.TankMuzzle, Symbols.TankTracks },
        { Symbols.TankTracks, Symbols.TankTurret, Symbols.TankTracks },
        { Symbols.TankTracks, Symbols.TankBack, Symbols.TankTracks },
    };
    
    public char[,] TankUpView => _tankUpView;
    public char[,] TankDownView => _tankDownView;
    public char[,] TankLeftView => _tankLeftView;
    public char[,] TankRightView => _tankRightView;
    
    
}