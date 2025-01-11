using System.Diagnostics;
using C_Charp_Tanks.Renderer;

namespace C_Charp_Tanks;

public class ConsoleRenderer : IRenderer
{
    private readonly char[,] _prevPixels;
    private readonly byte[,] _prevColorByte;
    public int width { get; private set; } // ширина консольного окна
    public int height { get; private set; } // высота консольного окна

    private const int MaxColors = 8; // ограничивает количество поддерживаемых цветов до 8.
    
    private readonly ConsoleColor[] _colors; // массив цветов, используемых для отрисовки
    public ConsoleColor bgColor { get; set; }  // хранит цвет фона консоли
    
    private readonly char[,] _pixels; // двумерный массив символов, представляющих пиксели в консоли
    private readonly byte[,] _pixelColors; // Двумерный массив индексов цветов, соответствующих каждому пикселю
    
    private readonly int _maxWidth; // максимальная ширина консольного окна, которые могут быть использованы для отрисовки.
    private readonly int _maxHeight; // Максимальная высота консольного окна, которые могут быть использованы для отрисовки.
    
    public bool IsRenderChanged = true;

    private ConsoleRenderer _currentRenderer;
    private ConsoleRenderer _previousRenderer;
    
    // Индексатор  Позволяет получить или установить символ для конкретной координаты пикселя (w, h), используя синтаксис renderer[w, h]
    public char this[int w, int h]
    {
        get { return _pixels[w, h]; }
        set { _pixels[w, h] = value; }
    }

    // Конструктор
    public ConsoleRenderer(ConsoleColor[] colors)
    {
        
        _currentRenderer = this;
        
        if (colors.Length > MaxColors) 
        {
            // Принимает массив цветов colors. Если количество цветов превышает MaxColors, они обрезаются до этого значения.
            var tmp = new ConsoleColor[MaxColors];
            Array.Copy(colors, tmp, tmp.Length);
            colors = tmp;
        }

        _colors = colors;

        _maxWidth = Console.LargestWindowWidth; // установка максимальной ширины экрана
        _maxHeight = Console.LargestWindowHeight; // установка максимальной высоты экрана
        width = Console.WindowWidth; // ширина консольного окна
        height = Console.WindowHeight; // высота консольного окна

        _pixels = new char[_maxWidth, _maxHeight]; // инициализация массива пикселей
        _pixelColors = new byte[_maxWidth, _maxHeight]; // инициализация цвета пикселей
        
        _prevColorByte = new byte[_maxWidth, _maxHeight];
        _prevPixels = new char[_maxWidth, _maxHeight];
    }

    // Устанавливает символ (val) и индекс цвета (colorIdx) для пикселя на координатах w, h
    public void SetPixel(int w, int h, char val, byte colorIdx)
    { 
        _pixels[w, h] = val;
        _pixelColors[w, h] = colorIdx;
    }


    // Отвечает за отрисовку содержимого массива _pixels в консоль.
    public void Render()
    {
        
        if (IsRenderChanged)
        {
            Console.Clear(); // очищает экран
            Console.BackgroundColor = bgColor; // Устанавливает фоновый цвет

            for (var w = 0; w < width; w++) // заполняем консоль
            for (var h = 0; h < height; h++)
            {
                var colorIdx = _pixelColors[w, h]; // цвета
                var color = _colors[colorIdx]; // 
                var symbol = _pixels[w, h]; // символы

                if (symbol == 0 || color == bgColor) // если нет символа или цвет совпадает с bgColor - пропускаем
                    continue;

                Console.ForegroundColor = color; // установка фонового цвета

                Console.SetCursorPosition(w, h); // установка курсора
                Console.Write(symbol); // прорисовка символа
            }

            Console.ResetColor(); // сбрасывает цветовые настройки консоли
            Console.CursorVisible = false; // скрывает курсор
        }
        
        
        /*PrevStatePixels();
        IsRenderChanged = CheckIsCurrentDifferentFromCache();*/
        
        _previousRenderer = _currentRenderer;
        
        //isRenderChanged = false;
    }

    private void PrevStatePixels()
    {
        for (int i = 0; i < _pixels.GetLength(0); i++)
        {
            for (int j = 0; j < _pixels.GetLength(1); j++)
            {
                _prevPixels[i, j] = _pixels[i, j];
            }
        }
        
        for (int i = 0; i < _pixelColors.GetLength(0); i++)
        {
            for (int j = 0; j < _pixelColors.GetLength(1); j++)
            {
                _prevColorByte[i, j] = _pixelColors[i, j];
            }
        }
    }

    private bool CheckIsCurrentDifferentFromCache()
    {
        
        for (int i = 0; i < _pixels.GetLength(0); i++)
        {
            for (int j = 0; j < _pixels.GetLength(1); j++)
            {
                Console.WriteLine($"prev = {_prevPixels[i, j]}, current = {_pixels[i, j]} ");
                if (_prevPixels[i, j] != _pixels[i, j])
                {
                    //Console.WriteLine("Pixels are different");
                    return true;
                }
                
            }
        }
        
        for (int i = 0; i < _pixelColors.GetLength(0); i++)
        {
            for (int j = 0; j < _pixelColors.GetLength(1); j++)
            {
                if (!_prevColorByte[i, j].Equals(_pixelColors[i, j]))
                {
                    Console.WriteLine("Colors are different");
                    return true;
                }

            }
        }
        
        return false;
    }

    // отрисовка текста в нужном месте
    public void DrawString(string text, int atWidth, int atHeight, ConsoleColor color)
    {
        var colorIdx = Array.IndexOf(_colors, color); // проверяет, есть ли цвет в массиве _colors
        if (colorIdx < 0) // если нет, то прерываем выполнение
            return;

        for (int i = 0; i < text.Length; i++)
        {
            _pixels[atWidth + i, atHeight] = text[i]; // заполняет каждый символ текста в нужную ячейку
            _pixelColors[atWidth + i, atHeight] = (byte)colorIdx; // заполняем каждую ячейку цветом по горизонтали
        }
    }

    // очистка консоли
    public void Clear()
    {
        for (int w = 0; w < width; w++)
        for (int h = 0; h < height; h++)
        {
            _pixelColors[w, h] = 0;
            _pixels[w, h] = (char)0;
        }
    }

    // Проверяет, идентичен ли данный объект другому экземпляру ConsoleRenderer
    // Сравнивает размеры окон и массив цветов.
    // Проверяет все элементы массивов _pixels и _pixelColors
    public override bool Equals(object? obj)
    {
        if (obj is not ConsoleRenderer casted)
            return false;

        if (_maxWidth != casted._maxWidth || _maxHeight != casted._maxHeight ||
            width != casted.width || height != casted.height ||
            _colors.Length != casted._colors.Length)
        {
            return false;
        }


        for (int i = 0; i < _colors.Length; i++)
        {
            if (_colors[i] != casted._colors[i])
                return false;
        }

        for (int w = 0; w < width; w++)
        for (var h = 0; h < height; h++)
        {
            if (_pixels[w, h] != casted._pixels[w, h] ||
                _pixelColors[w, h] != casted._pixelColors[w, h])
            {
                return false;
            }
        }

        return true;
    }

    // Генерирует уникальный хеш-код для объекта, учитывая размеры, массив цветов и значения пикселей
    public override int GetHashCode()
    {
        var hash = HashCode.Combine(_maxWidth, _maxHeight, width, height);

        for (int i = 0; i < _colors.Length; i++)
        {
            hash = HashCode.Combine(hash, _colors[i]);
        }

        for (int w = 0; w < width; w++)
        for (var h = 0; h < height; h++)
        {
            hash = HashCode.Combine(hash, _pixelColors[w, h], _pixels[w, h]);
        }

        return hash;
    }
}