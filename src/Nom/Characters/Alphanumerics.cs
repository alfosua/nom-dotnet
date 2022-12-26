using Nom.Sdk;

namespace Nom.Characters;

public interface IAlphanumericsParser<T> : IParser<T, T>
    where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>, IEmptyCheckable
{
}

public class AlphanumericsParser<T> : IAlphanumericsParser<T>
    where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>, IEmptyCheckable
{
    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.SplitWhenUnsatisfiedRequired<T, char>(input, char.IsLetterOrDigit, new()
        {
            ExceptionFactory = (_) => new InvalidOperationException("Could not parse letters or digits at head"),
        });
    }
}

public static class Alphanumerics
{
    public static IParser<StringParsable, StringParsable> Create() => new AlphanumericsParser<StringParsable>();
    
    public static IParser<T, T> Create<T>()
        where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>, IEmptyCheckable
        => new AlphanumericsParser<T>();
}
