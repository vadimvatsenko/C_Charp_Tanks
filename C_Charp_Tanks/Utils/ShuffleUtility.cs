namespace C_Sharp_Maze_Generator.Utils;

public class ShuffleUtility
{
    private static Random _random = new();
    public static void Shuffle<T>(T[,] data)
    {
        for (int i = 0; i < data.GetLength(0); i++)
        {
            int bufIndex = _random.Next(0, data.GetLength(0));
            if (bufIndex == i)
                continue;

            T bufX = data[bufIndex, 0];
            T bufY = data[bufIndex, 1];

            data[bufIndex, 0] = data[i, 0];
            data[bufIndex, 1] = data[i, 1];

            data[i, 0] = bufX;
            data[i, 1] = bufY;
        }
    }

    public static void Shuffle<T>(List<T> data)
    {
        for(int i = 0; i < data.Count; i++)
        {
            int bufIndex = _random.Next(0, data.Count);
            if (bufIndex == i)
                continue;

            var buf = data[bufIndex];
            data[bufIndex] = data[i];
            data[i] = buf;
        }
    }
}