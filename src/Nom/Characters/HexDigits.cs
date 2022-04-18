using Nom.Sdk;

namespace Nom.Characters;

public interface IHexDigitsParser<T> : IParser<T, T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
{
}

public class HexDigitsParser<T> : IHexDigitsParser<T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
{
    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.ParseByMatchingRegex(input, @"^([0-9abcdefABCDEF]+)", new()
        {
            ExceptionFactory = (_, _) => new InvalidOperationException("Could not parse any hexadecimal digits at head"),
        });
    }
}

public static class HexDigits
{
    public static IHexDigitsParser<StringParsable> Create() => new HexDigitsParser<StringParsable>();
    
    public static IHexDigitsParser<T> Create<T>()
        where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
        => new HexDigitsParser<T>();
}
