using Nom.Sdk;

namespace Nom.Characters;

public interface IDigitsParser<T> : IParser<T, T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
{
}

public class DigitsParser<T> : IDigitsParser<T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
{
    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.ParseByMatchingRegex(input, @"^([0-9]+)", new()
        {
            ExceptionFactory = (_, _) => new InvalidOperationException("Could not parse any digits at head"),
        });
    }
}

public static class Digits
{
    public static IParser<StringParsable, StringParsable> Create() => new DigitsParser<StringParsable>();
    
    public static IParser<T, T> Create<T>()
        where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
        => new DigitsParser<T>();
}
