using Nom.Sdk;

namespace Nom.Characters;

public interface IHexDigitsOrNoneParser<T> : IParser<T, T>
    where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>
{
}

public class HexDigitsOrNoneParser<T> : IHexDigitsOrNoneParser<T>
    where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>
{
    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.SplitWhenUnsatisfiedOptional<T, char>(input, CommonSatisfiers.IsHexDigit);
    }
}

public static class HexDigitsOrNone
{
    public static IParser<StringParsable, StringParsable> Create() => new HexDigitsOrNoneParser<StringParsable>();
    
    public static IParser<T, T> Create<T>()
        where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>
        => new HexDigitsOrNoneParser<T>();
}
