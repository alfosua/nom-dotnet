using Nom.Sdk;

namespace Nom.Characters;

public interface IOneOfParser<T> : IParser<T, T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
{
}

public class OneOfParser<T> : IOneOfParser<T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
{
    public OneOfParser(string target)
    {
        Target = target;
    }

    public string Target { get; }

    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.ParseByMatchingRegex(input, $@"^[{Target}]", new()
        {
            ExceptionFactory = (_, _) => new InvalidOperationException($"Could not parse one of '{Target}' at head"),
        });
    }
}

public static class OneOf
{
    public static IOneOfParser<StringParsable> Create(string target) => new OneOfParser<StringParsable>(target);

    public static IOneOfParser<T> Create<T>(string target)
        where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
        => new OneOfParser<T>(target);
}
