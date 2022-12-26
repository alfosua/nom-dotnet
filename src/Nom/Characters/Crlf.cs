using Nom.Sdk;

namespace Nom.Characters;

public interface ICrlfParser<T> : IParser<T, T>
    where T : IParsable, ISplitableAtPosition<T>, IContentVisitable<string>, IEmptyCheckable
{
}

public class CrlfParser<T> : ICrlfParser<T>
    where T : IParsable, ISplitableAtPosition<T>, IContentVisitable<string>, IEmptyCheckable
{
    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.SplitAtNextIfSatisfied<T, string>(input, c => c == "\r\n", new()
        {
            ExceptionFactory = (_) => new InvalidOperationException($"Could not parse CRLF at head"),
        });
    }
}

public static class Crlf
{
    public static IParser<StringParsable, StringParsable> Create() => new CrlfParser<StringParsable>();

    public static IParser<T, T> Create<T>()
        where T : IParsable, ISplitableAtPosition<T>, IContentVisitable<string>, IEmptyCheckable
        => new CrlfParser<T>();
}
