using Nom.Sdk;

namespace Nom.Characters;

public interface ILineEndingParser<T> : IParser<T, T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
{
}

public class LineEndingParser<T> : ILineEndingParser<T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
{
    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.ParseByMatchingRegex(input, @"^((\r|)\n)", new()
        {
            ExceptionFactory = (_, _) => new InvalidOperationException($"Could not parse line ending at head"),
        });
    }
}

public static class LineEnding
{
    public static IParser<StringParsable, StringParsable> Create() => new LineEndingParser<StringParsable>();

    public static IParser<T, T> Create<T>()
        where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
        => new LineEndingParser<T>();
}
