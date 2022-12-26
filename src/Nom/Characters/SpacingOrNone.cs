using Nom.Sdk;

namespace Nom.Characters;

public interface ISpacingOrNoneParser<T> : IParser<T, T>
    where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>
{
}

public class SpacingOrNoneParser<T> : ISpacingOrNoneParser<T>
    where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>
{
    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.SplitWhenUnsatisfiedOptional<T, char>(input, CommonSatisfiers.IsSpace);
    }
}

public static class SpacingOrNone
{
    public static IParser<StringParsable, StringParsable> Create() => new SpacingOrNoneParser<StringParsable>();

    public static IParser<T, T> Create<T>()
        where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>
        => new SpacingOrNoneParser<T>();
}
