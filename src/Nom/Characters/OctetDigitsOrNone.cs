using Nom.Sdk;

namespace Nom.Characters;

public interface IOctetDigitsOrNoneParser<T> : IParser<T, T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
{
}

public class OctetDigitsOrNoneParser<T> : IOctetDigitsOrNoneParser<T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
{
    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.ParseByMatchingRegex(input, @"^([0-7]*)", new()
        {
            ThrowWhenEmptyInput = false,
        });
    }
}

public static class OctetDigitsOrNone
{
    public static IOctetDigitsOrNoneParser<StringParsable> Create() => new OctetDigitsOrNoneParser<StringParsable>();

    public static IOctetDigitsOrNoneParser<T> Create<T>()
        where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
        => new OctetDigitsOrNoneParser<T>();
}
