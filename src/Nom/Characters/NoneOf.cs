using Nom.Sdk;

namespace Nom.Characters;

public interface INoneOfParser<T> : IParser<T, T>
    where T : IParsable, ISplitableAtPosition<T>, IContentVisitable<char>, IEmptyCheckable
{
}

public class NoneOfParser<T> : INoneOfParser<T>
    where T : IParsable, ISplitableAtPosition<T>, IContentVisitable<char>, IEmptyCheckable
{
    public NoneOfParser(string target)
    {
        Target = target;
    }

    public string Target { get; }

    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.SplitAtNextIfSatisfied<T, char>(input, ch => !Target.Contains(ch), new()
        {
            ExceptionFactory = (_) => new InvalidOperationException($"Could not parse anything but one of '{Target}' at head"),
        });
    }
}

public static class NoneOf
{
    public static IParser<StringParsable, StringParsable> Create(string target) => new NoneOfParser<StringParsable>(target);

    public static IParser<T, T> Create<T>(string target)
        where T : IParsable, ISplitableAtPosition<T>, IContentVisitable<char>, IEmptyCheckable
        => new NoneOfParser<T>(target);
}
