using Nom.Sdk;

namespace Nom.Characters;

public interface IMultispaceParser<T> : IParser<T, T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
{
}

public class MultispaceParser<T> : IMultispaceParser<T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
{
    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.ParseByMatchingRegex(input, @"^([\s\t\r\n]+)", new()
        {
            ExceptionFactory = (_, _) => new InvalidOperationException("Could not parse any multiple spacing at head"),
        });
    }
}

public static class Multispace
{
    public static IMultispaceParser<StringParsable> Create() => new MultispaceParser<StringParsable>();

    public static IMultispaceParser<T> Create<T>()
        where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
        => new MultispaceParser<T>();
}
