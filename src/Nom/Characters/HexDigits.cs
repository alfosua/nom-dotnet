using Nom.Sdk;

namespace Nom.Characters;

public interface IHexDigitsParser<T> : IParser<T, T>
    where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>, IEmptyCheckable
{
}

public class HexDigitsParser<T> : IHexDigitsParser<T>
    where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>, IEmptyCheckable
{
    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.SplitWhenUnsatisfiedRequired<T, char>(input, CommonSatisfiers.IsHexDigit, new()
        {
            ExceptionFactory = (_) => new InvalidOperationException("Could not parse hex digits at head"),
        });
    }
}

public static class HexDigits
{
    public static IParser<StringParsable, StringParsable> Create() => new HexDigitsParser<StringParsable>();
    
    public static IParser<T, T> Create<T>()
        where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>, IEmptyCheckable
        => new HexDigitsParser<T>();
}
