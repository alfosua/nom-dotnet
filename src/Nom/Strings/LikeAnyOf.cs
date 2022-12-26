using Nom.Sdk;

namespace Nom.Strings;

public interface ILikeAnyOfParser<T> : IParser<T, T>
    where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>, IEmptyCheckable
{
}

public class LikeAnyOfParser<T> : ILikeAnyOfParser<T>
    where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>, IEmptyCheckable
{
    public LikeAnyOfParser(string target)
    {
        Target = target;
    }

    public string Target { get; }

    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.SplitWhenUnsatisfiedRequired<T, char>(input, Target.Contains, new()
        {
            ExceptionFactory = (_) => new InvalidOperationException($"Could not parse like any of '{Target}' at head"),
        });
    }
}

public static class LikeAnyOf
{
    public static IParser<StringParsable, StringParsable> Create(string target) => new LikeAnyOfParser<StringParsable>(target);

    public static IParser<T, T> Create<T>(string target)
        where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>, IEmptyCheckable
        => new LikeAnyOfParser<T>(target);
}
