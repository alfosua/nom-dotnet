using Nom.Sdk;

namespace Nom.Characters;

public interface IAlphanumericsOrNoneParser<T> : IParser<T, T>
    where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>
{
}

public class AlphanumericsOrNoneParser<T> : IAlphanumericsOrNoneParser<T>
    where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>
{
    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.SplitWhenUnsatisfiedOptional<T, char>(input, char.IsLetterOrDigit);
    }
}

public static class AlphanumericsOrNone
{
    public static IParser<StringParsable, StringParsable> Create() => new AlphanumericsOrNoneParser<StringParsable>();
    
    public static IParser<T, T> Create<T>()
        where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>
        => new AlphanumericsOrNoneParser<T>();
}
