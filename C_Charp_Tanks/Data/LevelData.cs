namespace C_Charp_Tanks;

public class LevelData
{
    private static LevelData? _instance;

    private LevelData()
    {
        
    }

    public static LevelData? Instance
    {
        get 
        {
            if (_instance == null)
            {
                _instance = new LevelData();
            }
            return _instance;
        }
    }
    
    
}