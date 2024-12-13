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
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        int counter = 0;
        
        double accumulatedTime = 0f;
        
        while (_isRunning)
        {
           float deltaTime = (float)stopwatch.Elapsed.TotalSeconds;
           stopwatch.Restart();
           
           accumulatedTime += deltaTime;

           while (accumulatedTime >= _targetFrameTime)
           {
               UpdateItems(_targetFrameTime);
               accumulatedTime -= _targetFrameTime;
               counter++;
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