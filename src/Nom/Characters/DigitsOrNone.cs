using Nom.Sdk;

namespace Nom.Characters;

public interface IDigitsOrNoneParser<T> : IParser<T, T>
    where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>
{
}

public class DigitsOrNoneParser<T> : IDigitsOrNoneParser<T>
    where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>
{
    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.SplitWhenUnsatisfiedOptional<T, char>(input, char.IsDigit);
    }
}

public static class DigitsOrNone
{
    public static IParser<StringParsable, StringParsable> Create() => new DigitsOrNoneParser<StringParsable>();
    
    public static IParser<T, T> Create<T>()
        where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>
        => new DigitsOrNoneParser<T>();
}
