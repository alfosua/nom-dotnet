using Nom.Sdk;

namespace Nom.Characters;

public interface ILineEndingParser : IParser<string, string> { }

public class LineEndingParser : ILineEndingParser
{
    public IResult<string, string> Parse(string input)
    {
        return CommonParsings.ParseStringByMatchingRegex(input, @"^((\n)|(\r\n))", new()
        {
            ExceptionFactory = (_, _) => new InvalidOperationException("No line ending was matched at head"),
        });
    }
}

public static class LineEnding
{
    public static ILineEndingParser Create() => new LineEndingParser();
}
