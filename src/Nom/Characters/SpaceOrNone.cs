using Nom.Sdk;

namespace Nom.Characters;

public interface ISpaceOrNoneParser<T> : IParser<T, T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
{
}

public class SpaceOrNoneParser<T> : ISpaceOrNoneParser<T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
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
    public static IParser<StringParsable, StringParsable> Create() => new SpaceOrNoneParser<StringParsable>();

    public static IParser<T, T> Create<T>()
        where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
        => new SpaceOrNoneParser<T>();
}
