using System.Globalization;

namespace Presentation.Helpers;

public class InputValidator
{
    public static (int validatedInput, string? error) ParseInteger(string userInput, List<int> allowedRange)
    {
        if (int.TryParse(userInput, out int validatedInput) && allowedRange.Contains(validatedInput))
        {
            return (validatedInput, null);
        }
        return (0, "Invalid input");
    }

    public static (List<int> validatedInts, string? error) ParseIntegers(string userInput, List<int> allowedRange)
    {
        var inputValues = userInput.Split(",");
        var output = new List<int>();
        foreach (var input in inputValues)
        {
            if (int.TryParse(input, out int validatedInt) && allowedRange.Contains(validatedInt))
            {
                output.Add(validatedInt);
                continue;
            }
            return (output, $"There input {input} is not valid");
        }
        return (output, null);
    }

    public static (decimal validatedInput, string? error) ParseDecimal(string userInput, int minimumValue)
    {
        if (decimal.TryParse(userInput, out decimal validatedInput) && validatedInput >= minimumValue)
        {
            return (validatedInput, null);
        }
        return (0, "Invalid input");
    }

    public static (DateOnly validatedInput, string? error) ParseDate(string userInput)
    {
        if (DateOnly.TryParseExact(userInput, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly validatedInput))
        {
            return (validatedInput, null);
        }
        return (DateOnly.MinValue, "Date input not valid");
    }

    //public static (DateTime validatedInput, string? error) ParseDate(string userInput)
    //{
    //    if (DateTime.TryParseExact(userInput, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime validatedInput))
    //    {
    //        return (validatedInput, null);
    //    }
    //    return (new DateTime(), "Date input not valid");
    //}

    public static (string? validatedInput, string? error) ParseString(string userInput)
    {
        if (!string.IsNullOrEmpty(userInput))
        {
            return (userInput, null);
        }
        return (null, "Invalid input, the string is null or empty");
    }
}
