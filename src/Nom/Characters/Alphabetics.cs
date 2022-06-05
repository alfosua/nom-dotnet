using Nom.Sdk;

namespace Nom.Characters;

public interface IAlphabeticsParser<T> : IParser<T, T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
{
}

public class AlphabeticsParser<T> : IAlphabeticsParser<T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
{
    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.ParseByMatchingRegex(input, @"^([A-Za-z]+)", new()
        {
            ExceptionFactory = (_, _) => new InvalidOperationException("Could not parse any alphabetics at head"),
        });
    }
}

public static class Alphabetics
{
    public static IParser<StringParsable, StringParsable> Create() => new AlphabeticsParser<StringParsable>();
    
    public static IParser<T, T> Create<T>()
        where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
        => new AlphabeticsParser<T>();
}
