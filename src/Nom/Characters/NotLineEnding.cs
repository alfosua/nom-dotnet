using Nom.Sdk;

namespace Nom.Characters;

public interface INotLineEndingParser<T> : IParser<T, T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
{
}

public class NotLineEndingParser<T> : INotLineEndingParser<T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
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
    public static IParser<StringParsable, StringParsable> Create() => new NotLineEndingParser<StringParsable>();
    
    public static IParser<T, T> Create<T>()
        where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
        => new NotLineEndingParser<T>();
}
