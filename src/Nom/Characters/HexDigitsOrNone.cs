using Nom.Sdk;

namespace Nom.Characters;

public interface IHexDigitsOrNoneParser<T> : IParser<T, T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
{
}

public class HexDigitsOrNoneParser<T> : IHexDigitsOrNoneParser<T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
{
    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.ParseByMatchingRegex(input, @"^([0-9abcdefABCDEF]*)", new()
        {
            ThrowWhenEmptyInput = false,
        });
    }
}

public static class HexDigitsOrNone
{
    public static IHexDigitsOrNoneParser<StringParsable> Create() => new HexDigitsOrNoneParser<StringParsable>();
    
    public static IHexDigitsOrNoneParser<T> Create<T>()
        where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
        => new HexDigitsOrNoneParser<T>();
}
