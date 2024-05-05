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
    }
}
