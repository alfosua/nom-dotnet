using Nom.Sdk;

namespace Nom.Characters;

public interface INewLineParser<T> : IParser<T, T>
    where T : IParsable, ISplitableAtPosition<T>, IContentVisitable<char>, IEmptyCheckable
{
}

public class NewLineParser<T> : INewLineParser<T>
    where T : IParsable, ISplitableAtPosition<T>, IContentVisitable<char>, IEmptyCheckable
{
    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.SplitAtNextIfSatisfied<T, char>(input, c => c == '\n', new()
        {
            ExceptionFactory = (_) => new InvalidOperationException($"Could not parse any new line at head"),
        });
    }
}

public static class NewLine
{
    public static IParser<StringParsable, StringParsable> Create() => new NewLineParser<StringParsable>();
    
    public static IParser<T, T> Create<T>()
        where T : IParsable, ISplitableAtPosition<T>, IContentVisitable<char>, IEmptyCheckable
        => new NewLineParser<T>();
}
