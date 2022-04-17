using Nom.Sdk;

namespace Nom.Characters;

public interface IAlphabeticsOrNoneParser<T> : IParser<T, T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>
{
}

public class AlphabeticsOrNoneParser<T> : IAlphabeticsOrNoneParser<T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>
{
    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.ParseByMatchingRegex(input, @"^([A-Za-z]*)", new()
        {
            ThrowWhenEmptyInput = false,
        });
    }
}

public static class AlphabeticsOrNone
{
    public static IAlphabeticsOrNoneParser<StringParsable> Create() => new AlphabeticsOrNoneParser<StringParsable>();
    
    public static IAlphabeticsOrNoneParser<T> Create<T>()
        where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>
        => new AlphabeticsOrNoneParser<T>();
}
