using Nom.Sdk;

namespace Nom.Characters;

public interface ISpaceParser<T> : IParser<T, T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>
{
}

public class SpaceParser<T> : ISpaceParser<T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>
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
    public static ISpaceParser<StringParsable> Create() => new SpaceParser<StringParsable>();

    public static ISpaceParser<T> Create<T>()
        where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>
        => new SpaceParser<T>();
}
