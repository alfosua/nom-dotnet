using Nom.Sdk;

namespace Nom.Characters;

public interface ITabParser<T> : IParser<T, T>
    where T : IParsable, ISplitableAtPosition<T>, IContentVisitable<char>, IEmptyCheckable
{
}

public class TabParser<T> : ITabParser<T>
    where T : IParsable, ISplitableAtPosition<T>, IContentVisitable<char>, IEmptyCheckable
{
    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.SplitAtNextIfSatisfied<T, char>(input, c => c == '\t', new()
        {
            ExceptionFactory = (_) => new InvalidOperationException($"Could not parse any new line at head"),
        });
    }
}

public static class Tab
{
    public static IParser<StringParsable, StringParsable> Create() => new TabParser<StringParsable>();
    
    public static IParser<T, T> Create<T>()
        where T : IParsable, ISplitableAtPosition<T>, IContentVisitable<char>, IEmptyCheckable
        => new TabParser<T>();
}
