using Nom.Sdk;

namespace Nom.Strings;

public interface ITagParser<T> : IParser<T, T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
{
}

public class TagParser<T> : ITagParser<T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
{
    public TagParser(string target)
    {
        Target = target;
    }

    public string Target { get; }

    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.ParseByMatchingRegex(input, $@"^({Target})", new()
        {
            ExceptionFactory = (_, _) => new InvalidOperationException($"Could not parse tag '{Target}' at head"),
        });
    }
}

public static class Tag
{
    public static ITagParser<StringParsable> Create(string target) => new TagParser<StringParsable>(target);

    public static ITagParser<T> Create<T>(string target)
        where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
        => new TagParser<T>(target);
}
