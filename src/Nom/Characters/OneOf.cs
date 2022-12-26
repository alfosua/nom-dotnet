using Nom.Sdk;

namespace Nom.Characters;

public interface IOneOfParser<T> : IParser<T, T>
    where T : IParsable, ISplitableAtPosition<T>, IContentVisitable<char>, IEmptyCheckable
{
}

public class OneOfParser<T> : IOneOfParser<T>
    where T : IParsable, ISplitableAtPosition<T>, IContentVisitable<char>, IEmptyCheckable
{
    public OneOfParser(string target)
    {
        Target = target;
    }

    public string Target { get; }

    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.SplitAtNextIfSatisfied<T, char>(input, Target.Contains, new()
        {
            ExceptionFactory = (_) => new InvalidOperationException($"Could not parse anything but one of '{Target}' at head"),
        });
    }
}

public static class OneOf
{
    public static IParser<StringParsable, StringParsable> Create(string target) => new OneOfParser<StringParsable>(target);
    
    public static IParser<T, T> Create<T>(string target)
        where T : IParsable, ISplitableAtPosition<T>, IContentVisitable<char>, IEmptyCheckable
        => new OneOfParser<T>(target);
}
