using Nom.Sdk;

namespace Nom.Characters;

public interface IMultispaceOrNoneParser<T> : IParser<T, T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>
{
}

public class MultispaceOrNoneParser<T> : IMultispaceOrNoneParser<T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>
{
    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.ParseByMatchingRegex(input, @"^([\s\t\r\n]*)", new()
        {
            ThrowWhenEmptyInput = false,
        });
    }
}

public static class MultispaceOrNone
{
    public static IMultispaceOrNoneParser<StringParsable> Create() => new MultispaceOrNoneParser<StringParsable>();
    
    public static IMultispaceOrNoneParser<T> Create<T>()
        where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>
        => new MultispaceOrNoneParser<T>();
}
