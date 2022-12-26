using Nom.Sdk;

namespace Nom.Characters;

public interface IOctetDigitsOrNoneParser<T> : IParser<T, T>
    where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>
{
}

public class OctetDigitsOrNoneParser<T> : IOctetDigitsOrNoneParser<T>
    where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>
{
    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.SplitWhenUnsatisfiedOptional<T, char>(input, CommonSatisfiers.IsOctetDigit);
    }
}

public static class OctetDigitsOrNone
{
    public static IParser<StringParsable, StringParsable> Create() => new OctetDigitsOrNoneParser<StringParsable>();

    public static IParser<T, T> Create<T>()
        where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>
        => new OctetDigitsOrNoneParser<T>();
}
