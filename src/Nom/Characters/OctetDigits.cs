using Nom.Sdk;

namespace Nom.Characters;

public interface IOctetDigitsParser<T> : IParser<T, T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
{
}

public class OctetDigitsParser<T> : IOctetDigitsParser<T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
{
    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.ParseByMatchingRegex(input, @"^([0-7]+)", new()
        {
            ExceptionFactory = (_, _) => new InvalidOperationException("Could not parse any octet digits at head"),
        });
    }
}

public static class OctetDigits
{
    public static IParser<StringParsable, StringParsable> Create() => new OctetDigitsParser<StringParsable>();
    
    public static IParser<T, T> Create<T>()
        where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
        => new OctetDigitsParser<T>();
}
