using Nom.Sdk;

namespace Nom.Strings;

public interface ILikeAnyOfParser<T> : IParser<T, T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
{
}

public class LikeAnyOfParser<T> : ILikeAnyOfParser<T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
{
    public LikeAnyOfParser(string target)
    {
        Target = target;
    }

    public string Target { get; }

    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.ParseByMatchingRegex(input, $@"^([{Target}]+)", new()
        {
            ExceptionFactory = (_, _) => new InvalidOperationException($"Could not parse anything like any of '{Target}' at head"),
        });
    }
}

public static class LikeAnyOf
{
    public static ILikeAnyOfParser<StringParsable> Create(string target) => new LikeAnyOfParser<StringParsable>(target);

    public static ILikeAnyOfParser<T> Create<T>(string target)
        where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
        => new LikeAnyOfParser<T>(target);
}
