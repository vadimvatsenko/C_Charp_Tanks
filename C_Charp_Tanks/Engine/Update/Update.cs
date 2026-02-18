using System.Diagnostics;
using C_Charp_Tanks.Engine.Renderer;

namespace C_Charp_Tanks.Engine.Update;

public class Update
{
    private List<IUpdatable> _updatableList = new List<IUpdatable>();
    private char[][,] _layersArray;
    private BaseRenderer _renderer;
    private MapConfig _mapConfig;

    // Stopwatch — точніший таймер, ніж DateTime.Now
    private Stopwatch _sw = Stopwatch.StartNew();

    // === Налаштування цільового FPS ============================================
    private int _targetFps;

    // Час одного кадру в мілісекундах.
    // 1000 мс / 60 ≈ 16.666... мс на кадр
    private double _targetFrameMs;

    // Час, коли "має" закінчитися наступний кадр (у мс від старту програми)
    private double _nextFrameMs = 0;

    // === deltaTime ==============================================================
    // Зберігаємо час старту попереднього кадру (в мс)
    private double _lastFrameStartMs;

    // === Лічильники для FPS =====================================================
    private double _fps = 0;
    private int _frames = 0; // скільки кадрів пройшло за поточну секунду

    private double _fpsTimerStartMs; // старт відліку "секунди"

    public Update(int targetFps, BaseRenderer renderer, MapConfig mapConfig, params char[][,] layers )
    {
        _renderer = renderer;
        _layersArray = layers;
        _mapConfig = mapConfig;
        _targetFps = targetFps;
        _targetFrameMs = 1000.0 / _targetFps; // 1000 мс / 60 ≈ 16.666... мс на кадр

        _lastFrameStartMs = _sw.Elapsed.TotalMilliseconds; // Зберігаємо час старту попереднього кадру (в мс)
        _fpsTimerStartMs = _sw.Elapsed.TotalMilliseconds; // старт відліку "секунди"
    }

    public void AddUpdatable(params IUpdatable[] updatable) => _updatableList.AddRange(updatable);
    
    public void RunUpdate()
    {
        while (true)
        {

            // === 1) ПОЧАТОК КАДРУ ==================================================
            double frameStartMs = _sw.Elapsed.TotalMilliseconds;

            // deltaTime — час між стартом цього кадру і стартом попереднього кадру
            // В секундах (бо в іграх dt зазвичай у секундах)
            double deltaTime = (frameStartMs - _lastFrameStartMs) / 1000.0;

            // Оновлюємо "останній час" для наступної ітерації
            _lastFrameStartMs = frameStartMs;

            // Тут зазвичай викликають ігрову логіку:
            // Update(deltaTime);

            ///////////////////////////////
            // порядок важливий

            // початок з першого, щоб не витерти background
            
            for (int i = 1; i < _layersArray.Length; i++)
            {
                _renderer.Clear(_layersArray[i]);
            }
            
            foreach (var u in _updatableList)
            {
                u.Update(deltaTime);
            }

            _renderer.DrawString(_layersArray[0], 0, 0, $"FPS: {_fps:F2} | deltaTime: {deltaTime:F4}s");
            var frame = _renderer.Compose(_mapConfig.Width, _mapConfig.Height, _layersArray);
            _renderer.Render(frame);
            
            ////////////////////////////////

            // === 2) ОБМЕЖЕННЯ FPS ===================================================
            // Ми хочемо, щоб кожен кадр закінчувався не раніше, ніж через targetFrameMs
            // Тому плануємо "час завершення" поточного кадру:
            _nextFrameMs += _targetFrameMs;

            // Скільки часу ще залишилось до nextFrameMs
            double nowMs = _sw.Elapsed.TotalMilliseconds;
            double remainingMs = _nextFrameMs - nowMs;

            // Якщо залишилось більше ~1 мс — можна приспати потік (економить CPU)
            if (remainingMs > 1)
            {
                // Sleep приймає ціле число мілісекунд -> дробова частина губиться
                Thread.Sleep((int)remainingMs);
            }

            // Доробляємо "точність" коротким активним очікуванням (busy-wait)
            // Це дає рівніший таймінг, ніж один лише Sleep
            while (_sw.Elapsed.TotalMilliseconds < _nextFrameMs)
            {
                // нічого не робимо — просто чекаємо
            }

            // === 3) ПІДРАХУНОК FPS (раз на ~1 секунду) ==============================
            _frames++;

            double passedMs = _sw.Elapsed.TotalMilliseconds - _fpsTimerStartMs;

            // Якщо пройшла (або майже) секунда — виводимо FPS
            if (passedMs >= 1000)
            {
                _fps = _frames / (passedMs / 1000.0);

                // Показуємо також останній deltaTime для наочності
                //Console.WriteLine($"FPS: {_fps:F2} | deltaTime: {deltaTime:F4}s");
                // Скидаємо лічильники на наступну секунду
                _frames = 0;
                _fpsTimerStartMs = _sw.Elapsed.TotalMilliseconds;
            }
        }
    }
}