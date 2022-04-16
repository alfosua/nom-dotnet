using Nom.Sdk;

namespace Nom.Strings;

public interface ITagParser : IParser<string, string> { }

public class TagParser : ITagParser
{
    public TagParser(string target)
    {
        Target = target;
    }

    public string Target { get; }

    public IResult<string, string> Parse(string input)
    {
        return CommonParsings.ParseStringByMatchingRegex(input, $@"^({Target})", new()
        {
            ExceptionFactory = (_, _) => new InvalidOperationException($"Could not parse tag '{Target}' at head"),
        });
    }
}

public static class Tag
{
    public static ITagParser Create(string target) => new TagParser(target);
}
