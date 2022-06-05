using Nom.Sdk;

namespace Nom.Characters;

public interface IDigitsOrNoneParser<T> : IParser<T, T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
{
}

public class DigitsOrNoneParser<T> : IDigitsOrNoneParser<T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
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
    public static IParser<StringParsable, StringParsable> Create() => new DigitsOrNoneParser<StringParsable>();
    
    public static IParser<T, T> Create<T>()
        where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
        => new DigitsOrNoneParser<T>();
}
