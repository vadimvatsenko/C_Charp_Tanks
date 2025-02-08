using System.Data;
using System.Diagnostics;

namespace C_Charp_Tanks;

public class Update
{
    private const float TARGETFRAMETIME = 1 / 60f;
    private DateTime lastFrameTime;

    private HashSet<IUpdatable> _updates;
    
    ConsoleRenderer _prevRenderer;
    ConsoleRenderer _currentRenderer;

    public Update(ConsoleRenderer prevRenderer, ConsoleRenderer currentRenderer)
    {
        _updates = new HashSet<IUpdatable>();
        _prevRenderer = prevRenderer;
        _currentRenderer = currentRenderer;
    }

    public void AddUpdateListener(IUpdatable listener)
    {
        _updates.Add(listener);
        Console.WriteLine($"Adding update listener: {listener.GetType().Name}");
    }

    public void RemoveUpdateListener(IUpdatable listener)
    {
        _updates.Remove(listener);
    }

    public void RemoveAllUpdateListeners()
    {
        _updates.Clear();
    }

    public void StartUpdate()
    {
        lastFrameTime = DateTime.Now;
        while (true)
        {
            DateTime frameStartTime = DateTime.Now;

            float deltaTime = (float)(frameStartTime - lastFrameTime).TotalSeconds;
            
            //aforeach (IUpdatable listener in _updates) listener.Update(deltaTime);
            
            lastFrameTime = frameStartTime;
            
            if(!_currentRenderer.Equals(_prevRenderer)) _currentRenderer.Render();
            
            ConsoleRenderer tmp = _prevRenderer;
            _prevRenderer = _currentRenderer;
            _currentRenderer = tmp;
            _currentRenderer.Clear();

            DateTime nextFrameTime = frameStartTime + TimeSpan.FromSeconds(TARGETFRAMETIME);

            DateTime endFrameTime = DateTime.Now;

            if (nextFrameTime > endFrameTime)
            {
                Thread.Sleep((int)(nextFrameTime - endFrameTime).TotalMilliseconds);
            }
        }
    }
}