using System.Diagnostics;
using System.Xml;

namespace C_Charp_Tanks;

public class Update
{
    private readonly double _targetFrameTime = 1f / 60f;

    private List<IUpdatable> iUpdatableList = new List<IUpdatable>();
    
    private bool _isRunning;

    public void Start()
    {
        _isRunning = true;
        RunGameLoop();
    }

    public void Stop()
    {
        _isRunning = false;
    }
    
    public void RegisterListener(IUpdatable iUpdatable)
    {
        iUpdatableList.Add(iUpdatable);
    }

    public void RemoveListener(IUpdatable iUpdatable)
    {
        iUpdatableList.Remove(iUpdatable);
    }
   
    public void RunGameLoop()
    {
        Stopwatch stopwatch = new Stopwatch(); // 1
        stopwatch.Start(); // 2
        
        double accumulatedTime = 0f; // 3
        
        while (_isRunning) // 4
        {
           float deltaTime = (float)stopwatch.Elapsed.TotalSeconds; // 5 - Получает общее затраченное время, измеренное текущим экземпляром.
           stopwatch.Restart(); // 6
           
           accumulatedTime += deltaTime; // 0.00016 + 0 

           while (accumulatedTime >= _targetFrameTime) // _targetFrameTime = 0.016
           {
               UpdateItems(_targetFrameTime); 
               accumulatedTime -= _targetFrameTime; // отнимаем один кадр
           }
           
           int sleepTime = (int)(accumulatedTime - _targetFrameTime) * 1000;

           if (sleepTime > 0)
           {
               Thread.Sleep(sleepTime);
           }
        }
    }

    private void UpdateItems(double deltaTime)
    {
        foreach (var up in iUpdatableList)
        {
            up.Update(deltaTime);
        }
    }
}