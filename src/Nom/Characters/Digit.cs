using Nom.Sdk;

namespace Nom.Characters;

public interface IDigitParser : IParser<string, string> { }

public class DigitParser : IDigitParser
{
    public IResult<string, string> Parse(string input)
    {
        return CommonParsings.ParseStringByMatchingRegex(input, @"^([0-9]+)", new()
        {
            ExceptionFactory = (_, _) => new InvalidOperationException($"Could not parse digits at head"),
        });
    }
}

public static class Digit
{
    public static IDigitParser Create() => new DigitParser();
}
