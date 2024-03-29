namespace Presentation.Implementations;

public class ItemsLogger<T> where T : class
{
    public static void PrintItems(IEnumerable<T> items)
    {
        var index = 1;
        foreach (var item in items)
        {
            Console.WriteLine($"{index}.- {item}");
            index++;
        }

        //for (int i = 0; i < items.Count; i++)
        //{
        //    Console.WriteLine($"{i + 1}.- {items[i]}");
        //}
    }
}
