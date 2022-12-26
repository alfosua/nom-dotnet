using Nom.Sdk;

namespace Nom.Characters;

public interface ISatisfiedByParser<T> : IParser<T, T>
    where T : IParsable, ISplitableAtPosition<T>, IContentVisitable<char>, IEmptyCheckable
{
}

public class SatisfiedByParser<T> : ISatisfiedByParser<T>
    where T : IParsable, ISplitableAtPosition<T>, IContentVisitable<char>, IEmptyCheckable
{
    public SatisfiedByParser(Func<char, bool> predicate)
    {
        Predicate = predicate;
    }

    public Func<char, bool> Predicate { get; }

    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.SplitAtNextIfSatisfied(input, Predicate, new()
        {
            ExceptionFactory = (_) => new InvalidOperationException("Could not parse by satisfying predicate at head"),
        });
    }
}

public static class SatisfiedBy
{
    public static IParser<StringParsable, StringParsable> Create(Func<char, bool> predicate) => new SatisfiedByParser<StringParsable>(predicate);
    
    public static IParser<T, T> Create<T>(Func<char, bool> predicate)
        where T : IParsable, ISplitableAtPosition<T>, IContentVisitable<char>, IEmptyCheckable
        => new SatisfiedByParser<T>(predicate);
}
