using C_Charp_Tanks;

public class Program
{
    const float targetFrameTime = 1f / 60f;

    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Создаём корневой объект, который содержит все зависимости
        CompositionRoot compositionRoot = new CompositionRoot();

        DateTime lastFrameTime = DateTime.Now;

        while (true)
        {
            DateTime frameStartTime = DateTime.Now;
            float deltaTime = (float)(frameStartTime - lastFrameTime).TotalSeconds;

            // Обновляем ввод
            compositionRoot.ConsoleInput.Update(deltaTime);

            // Рисуем новое состояние игры
            compositionRoot.TankGameplayLogic.DrawNewState(deltaTime, compositionRoot.CurrentRenderer);
            lastFrameTime = frameStartTime;

            // Рендерим кадр, если рендерер сменился
            if (!compositionRoot.CurrentRenderer.Equals(compositionRoot.PrevRenderer))
            {
                compositionRoot.CurrentRenderer.Render();
            }

            // Меняем рендереры местами
            compositionRoot.SwapRenderers();

            // Ограничение FPS
            DateTime nextFrameTime = frameStartTime + TimeSpan.FromSeconds(targetFrameTime);
            DateTime endFrameTime = DateTime.Now;

            if (nextFrameTime > endFrameTime)
            {
                Thread.Sleep((int)(nextFrameTime - endFrameTime).TotalMilliseconds);
            }
        }
    }
}

