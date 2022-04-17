using Nom.Sdk;

namespace Nom.Characters;

public interface INotLineEndingParser<T> : IParser<T, T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>
{
}

public class NotLineEndingParser<T> : INotLineEndingParser<T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>
{
    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.ParseByMatchingRegex(input, @"^(([^\r]|)[^\n])*", new()
        {
            ThrowWhenEmptyInput = false,
        });
    }
}

public static class NotLineEnding
{
    public static INotLineEndingParser<StringParsable> Create() => new NotLineEndingParser<StringParsable>();
    
    public static INotLineEndingParser<T> Create<T>()
        where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>
        => new NotLineEndingParser<T>();
}
