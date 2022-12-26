using Nom.Sdk;

namespace Nom.Characters;

public interface ISpacingParser<T> : IParser<T, T>
    where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>, IEmptyCheckable
{
}

public class SpacingParser<T> : ISpacingParser<T>
    where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>, IEmptyCheckable
{
    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.SplitWhenUnsatisfiedRequired<T, char>(input, CommonSatisfiers.IsSpace, new()
        {
            ExceptionFactory = (_) => new InvalidOperationException("Could not parse any spacing at head"),
        });
    }
}

public static class Spacing
{
    public static IParser<StringParsable, StringParsable> Create() => new SpacingParser<StringParsable>();

    public static IParser<T, T> Create<T>()
        where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>, IEmptyCheckable
        => new SpacingParser<T>();
}
