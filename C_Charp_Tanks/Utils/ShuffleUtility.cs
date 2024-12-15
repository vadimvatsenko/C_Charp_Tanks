namespace C_Charp_Tanks;

public static class ShuffleUtility
{
    private static Random random = new Random();

    // Перемешивание списка или одномерного массива
    public static void Shuffle<T>(IList<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

    // Перемешивание двумерного массива
    public static void Shuffle<T>(T[,] array)
    {
        int rows = array.GetLength(0);

        // Создаём список индексов строк
        List<int> indices = new List<int>();
        for (int i = 0; i < rows; i++)
        {
            indices.Add(i);
        }

        // Перемешиваем индексы строк
        Shuffle(indices);

        // Создаём копию перемешанного массива
        T[,] shuffledArray = new T[rows, array.GetLength(1)];
        for (int i = 0; i < rows; i++)
        {
            int sourceRow = indices[i];
            for (int j = 0; j < array.GetLength(1); j++)
            {
                shuffledArray[i, j] = array[sourceRow, j];
            }
        }

        // Копируем данные обратно в оригинальный массив
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                array[i, j] = shuffledArray[i, j];
            }
        }
    }
}