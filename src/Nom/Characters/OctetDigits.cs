using Nom.Sdk;

namespace Nom.Characters;

public interface IOctetDigitsParser<T> : IParser<T, T>
    where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>, IEmptyCheckable
{
}

public class OctetDigitsParser<T> : IOctetDigitsParser<T>
    where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>, IEmptyCheckable
{
    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.SplitWhenUnsatisfiedRequired<T, char>(input, CommonSatisfiers.IsOctetDigit, new()
        {
            ExceptionFactory = (_) => new InvalidOperationException("Could not parse octet digits at head"),
        });
    }
}

public static class OctetDigits
{
    public static IParser<StringParsable, StringParsable> Create() => new OctetDigitsParser<StringParsable>();
    
    public static IParser<T, T> Create<T>()
        where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>, IEmptyCheckable
        => new OctetDigitsParser<T>();
}
