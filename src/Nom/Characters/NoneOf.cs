using Nom.Sdk;

namespace Nom.Characters;

public interface INoneOfParser<T> : IParser<T, T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>
{
}

public class NoneOfParser<T> : INoneOfParser<T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>
{
    public NoneOfParser(string target)
    {
        Target = target;
    }

    public string Target { get; }

    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.ParseByMatchingRegex(input, $@"^[^{Target}]", new()
        {
            ExceptionFactory = (_, _) => new InvalidOperationException($"Could not parse anything but one of '{Target}' at head"),
        });
    }
}

public static class NoneOf
{
    public static INoneOfParser<StringParsable> Create(string target) => new NoneOfParser<StringParsable>(target);

    public static INoneOfParser<T> Create<T>(string target)
        where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>
        => new NoneOfParser<T>(target);
}
