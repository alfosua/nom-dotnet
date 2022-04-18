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
        return CommonParsings.ParseByPredicatingNextContent<T, string>(input, c => c == "\r\n", new()
        {
            ExceptionFactory = (_) => new InvalidOperationException($"Could not parse CRLF at head"),
        });
    }
}

public static class Crlf
{
    public static ICrlfParser<StringParsable> Create() => new CrlfParser<StringParsable>();

    public static ICrlfParser<T> Create<T>()
        where T : IParsable, ISplitableAtPosition<T>, IContentVisitable<string>, IEmptyCheckable
        => new CrlfParser<T>();
}
