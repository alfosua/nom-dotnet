using Nom.Sdk;

namespace Nom.Strings;

public interface INotLikeAnyOfParser<T> : IParser<T, T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
{
}

public class NotLikeAnyOfParser<T> : INotLikeAnyOfParser<T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
{
    public NotLikeAnyOfParser(string target)
    {
        Target = target;
    }

    public string Target { get; }

    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.ParseByMatchingRegex(input, $@"^([^{Target}]+)", new()
        {
            ExceptionFactory = (_, _) => new InvalidOperationException($"Could not parse anything but like any of '{Target}' at head"),
        });
    }
}

public static class NotLikeAnyOf
{
    public static INotLikeAnyOfParser<StringParsable> Create(string target) => new NotLikeAnyOfParser<StringParsable>(target);

    public static INotLikeAnyOfParser<T> Create<T>(string target)
        where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
        => new NotLikeAnyOfParser<T>(target);
}
