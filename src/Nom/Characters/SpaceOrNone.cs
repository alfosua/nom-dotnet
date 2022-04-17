using Nom.Sdk;

namespace Nom.Characters;

public interface ISpaceOrNoneParser<T> : IParser<T, T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>
{
}

public class SpaceOrNoneParser<T> : ISpaceOrNoneParser<T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>
{
    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.ParseByMatchingRegex(input, @"^([\s\t]*)", new()
        {
            ThrowWhenEmptyInput = false,
        });
    }
}

public static class SpaceOrNone
{
    public static ISpaceOrNoneParser<StringParsable> Create() => new SpaceOrNoneParser<StringParsable>();

    public static ISpaceOrNoneParser<T> Create<T>()
        where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>
        => new SpaceOrNoneParser<T>();
}
