using Nom.Sdk;

namespace Nom.Characters;

public interface ITabParser<T> : IParser<T, T>
    where T : IParsable, ISplitableAtPosition<T>, IContentVisitable<char>
{
}

public class TabParser<T> : ITabParser<T>
    where T : IParsable, ISplitableAtPosition<T>, IContentVisitable<char>
{
    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.ParseByPredicatingNextContent<T, char>(input, c => c == '\t', new()
        {
            ExceptionFactory = (_) => new InvalidOperationException($"Could not parse any new line at head"),
        });
    }
}

public static class Tab
{
    public static ITabParser<StringParsable> Create() => new TabParser<StringParsable>();

    public static ITabParser<T> Create<T>()
        where T : IParsable, ISplitableAtPosition<T>, IContentVisitable<char>
        => new TabParser<T>();
}
