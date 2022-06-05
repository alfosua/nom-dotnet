using Nom.Sdk;

namespace Nom.Characters;

public interface INoneOfParser<T> : IParser<T, T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
{
}

public class NoneOfParser<T> : INoneOfParser<T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
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
    public static IParser<StringParsable, StringParsable> Create(string target) => new NoneOfParser<StringParsable>(target);

    public static IParser<T, T> Create<T>(string target)
        where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
        => new NoneOfParser<T>(target);
}
