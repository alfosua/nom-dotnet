using Nom.Sdk;

namespace Nom.Characters;

public interface IAlphanumericsOrNoneParser<T> : IParser<T, T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
{
}

public class AlphanumericsOrNoneParser<T> : IAlphanumericsOrNoneParser<T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
{
    public IResult<T, T> Parse(T input)
    {

        return CommonParsings.ParseByMatchingRegex(input, @"^([0-9A-Za-z]*)", new()
        {
            ThrowWhenEmptyInput = false,
        });
    }
}

public static class AlphanumericsOrNone
{
    public static IAlphanumericsOrNoneParser<StringParsable> Create() => new AlphanumericsOrNoneParser<StringParsable>();
    
    public static IAlphanumericsOrNoneParser<T> Create<T>()
        where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
        => new AlphanumericsOrNoneParser<T>();
}
