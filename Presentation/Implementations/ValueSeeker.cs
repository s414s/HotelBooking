using Presentation.Helpers;

namespace Presentation.Implementations;

public static class ValueSeeker
{
    public static DateOnly AskForDate(string consoleText)
    {
        Console.WriteLine($"{consoleText}.");
        while (true)
        {
            (DateOnly validatedInput, string? error) = InputValidator.ParseDate(Console.ReadLine() ?? "");
            if (error is null)
            {
                return validatedInput;
            }
            Console.WriteLine(error, "Please make sure your input is correct");
        }
    }

    public static int AskForInteger(string consoleText, List<int> allowedRange)
    {
        var values = string.Join(", ", allowedRange);
        Console.WriteLine($"{consoleText}.");
        Console.WriteLine($"It must be one of the following integers: {values}.");
        while (true)
        {
            (int validatedInput, string? error) = InputValidator.ParseInteger(Console.ReadLine() ?? "", allowedRange);
            if (error is null)
            {
                return validatedInput;
            }
            Console.WriteLine(error, "Please make sure your input is correct");
        }
    }

    public static List<int> AskForIntegers(string consoleText, List<int> allowedRange)
    {
        var allowedValues = string.Join(", ", allowedRange);
        Console.WriteLine($"{consoleText}.");
        Console.WriteLine($"It must be one or more numbers, followed my a coma: {allowedValues}.");
        while (true)
        {
            (List<int> validatedInput, string? error) = InputValidator.ParseIntegers(Console.ReadLine() ?? "", allowedRange);
            if (error is null)
            {
                return validatedInput;
            }
            Console.WriteLine(error, "Please make sure your input is correct");
        }
    }

    public static decimal AskForDecimal(string consoleText, int minValue)
    {
        Console.WriteLine($"{consoleText}.");
        Console.WriteLine($"It must be a decimal number greater or equal to {minValue}.");
        while (true)
        {
            (decimal validatedInput, string? error) = InputValidator.ParseDecimal(Console.ReadLine() ?? "", minValue);
            if (error is null)
            {
                return validatedInput;
            }
            Console.WriteLine(error, "Please make sure your input is correct");
        }
    }

    public static string AskForString(string consoleText)
    {
        Console.WriteLine($"{consoleText}. It must be a valid string");
        while (true)
        {
            (string? validatedInput, string? error) = InputValidator.ParseString(Console.ReadLine() ?? "");
            if (error is null)
            {
                return validatedInput ?? string.Empty;
            }
            Console.WriteLine(error, "Please make sure your input is a positive integer");
        }
    }
}
