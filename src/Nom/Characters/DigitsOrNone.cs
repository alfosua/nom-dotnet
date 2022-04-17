using Nom.Sdk;

namespace Nom.Characters;

public interface IDigitsOrNoneParser<T> : IParser<T, T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>
{
}

public class DigitsOrNoneParser<T> : IDigitsOrNoneParser<T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>
{
    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.ParseByMatchingRegex(input, @"^([0-9]*)", new()
        {
            ThrowWhenEmptyInput = false,
        });
    }
}

public static class DigitsOrNone
{
    public static IDigitsOrNoneParser<StringParsable> Create() => new DigitsOrNoneParser<StringParsable>();
    
    public static IDigitsOrNoneParser<T> Create<T>()
        where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>
        => new DigitsOrNoneParser<T>();
}
