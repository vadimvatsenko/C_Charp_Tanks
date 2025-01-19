namespace C_Charp_Tanks;

public class GameData
{
    private static GameData _instance;

    private GameData()
    {
        
    }

    public static GameData Instance
    {
        get 
        {
            if (_instance == null)
            {
                _instance = new GameData();
            }
            return _instance;
        }
    }
    

    
    private char[,] _tank = new char[,]
    {
        { Symbols.TankTracks, Symbols.TankTracks, Symbols.TankTracks },
        { Symbols.TankMuzzle, Symbols.TankTurret, Symbols.TankBack },
        { Symbols.TankTracks, Symbols.TankTracks, Symbols.TankTracks },
    };
    
    public char[,] Tank => _tank;
}