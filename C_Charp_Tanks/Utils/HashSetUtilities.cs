namespace C_Sharp_Maze_Generator.Utils;

public class HashSetUtilities
{
    private static Random _random = new();
    public static T GetAndRemoveRandomElement<T>(HashSet<T> data)
    {
        
        int elementIndex = _random.Next(0, data.Count);
        int i = 0;

        foreach (var element in data)
        {
            if (i == elementIndex)
            {
                data.Remove(element);
                return element;
            }

            i++;
        }

        return default(T);
    }
}