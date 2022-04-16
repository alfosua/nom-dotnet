using Nom.Sdk;

namespace Nom.Characters;

public interface IAlphabetParser : IParser<string, string> { }

public class AlphabetParser : IAlphabetParser
{
    public IResult<string, string> Parse(string input)
    {
        return CommonParsings.ParseStringByMatchingRegex(input, @"^([A-Za-z]+)", new()
        {
            ExceptionFactory = (_, _) => new InvalidOperationException("No alphabetic characters were matched at head"),
        });
    }
}

public static class Alphabet
{
    public static IAlphabetParser Create() => new AlphabetParser();
}
