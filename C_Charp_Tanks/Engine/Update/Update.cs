using System.Data;
using System.Diagnostics;

namespace C_Charp_Tanks;

public class Update
{
    private const float TARGETFRAMETIME = 1 / 60f;
    private DateTime lastFrameTime;

    private HashSet<IUpdatable> _updates;

    public Update()
    {
        _updates = new HashSet<IUpdatable>();
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
            
            foreach (IUpdatable listener in _updates) listener.Update(deltaTime);
            
            lastFrameTime = frameStartTime;

            DateTime nextFrameTime = frameStartTime + TimeSpan.FromSeconds(TARGETFRAMETIME);

            DateTime endFrameTime = DateTime.Now;

            if (nextFrameTime > endFrameTime)
            {
                Thread.Sleep((int)(nextFrameTime - endFrameTime).TotalMilliseconds);
            }
        }
    }
}