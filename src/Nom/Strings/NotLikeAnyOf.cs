using Nom.Sdk;

namespace Nom.Strings;

public interface INotLikeAnyOfParser<T> : IParser<T, T>
    where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>, IEmptyCheckable
{
}

public class NotLikeAnyOfParser<T> : INotLikeAnyOfParser<T>
    where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>, IEmptyCheckable
{
    public NotLikeAnyOfParser(string target)
    {
        Target = target;
    }

    public string Target { get; }

    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.SplitWhenUnsatisfiedOptional<T, char>(input, ch => !Target.Contains(ch));
    }
}

public static class NotLikeAnyOf
{
    public static IParser<StringParsable, StringParsable> Create(string target) => new NotLikeAnyOfParser<StringParsable>(target);

    public static IParser<T, T> Create<T>(string target)
        where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>, IEmptyCheckable
        => new NotLikeAnyOfParser<T>(target);
}
