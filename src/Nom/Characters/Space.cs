using Nom.Sdk;

namespace Nom.Characters;

public interface ISpaceParser<T> : IParser<T, T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
{
}

public class SpaceParser<T> : ISpaceParser<T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
{
    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.ParseByMatchingRegex(input, @"^([\s\t]+)", new()
        {
            ExceptionFactory = (_, _) => new InvalidOperationException("Could not parse any spacing at head"),
        });
    }
}

public static class Space
{
    public static IParser<StringParsable, StringParsable> Create() => new SpaceParser<StringParsable>();

    public static IParser<T, T> Create<T>()
        where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
        => new SpaceParser<T>();
}
