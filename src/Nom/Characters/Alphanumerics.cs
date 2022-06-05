using Nom.Sdk;

namespace Nom.Characters;

public interface IAlphanumericsParser<T> : IParser<T, T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
{
}

public class AlphanumericsParser<T> : IAlphanumericsParser<T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
{
    public IResult<T, T> Parse(T input)
    {

        return CommonParsings.ParseByMatchingRegex(input, @"^([0-9A-Za-z]+)", new()
        {
            ExceptionFactory = (_, _) => new InvalidOperationException("Could not parse any alphanumerics at head"),
        });
    }
}

public static class Alphanumerics
{
    public static IParser<StringParsable, StringParsable> Create() => new AlphanumericsParser<StringParsable>();
    
    public static IParser<T, T> Create<T>()
        where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
        => new AlphanumericsParser<T>();
}
