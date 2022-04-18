using Nom.Sdk;

namespace Nom.Strings;

public delegate bool TakeTillRequiredPredicate(char value);

public interface ITakeTillRequiredParser<T> : IParser<T, T>
    where T : IParsable, ISplitableAtPosition<T>, IContentEnumerable<char>, IEmptyCheckable
{
}

public class TakeTillRequiredParser<T> : ITakeTillRequiredParser<T>
    where T : IParsable, ISplitableAtPosition<T>, IContentEnumerable<char>, IEmptyCheckable
{
    public TakeTillRequiredParser(TakeTillRequiredPredicate predicate)
    {
        Predicate = predicate;
    }

    public TakeTillRequiredPredicate Predicate { get; }

    public IResult<T, T> Parse(T input)
    {
        CommonParsings.CheckInputIsNotEmpty(input);
        
        int count = 0;

        foreach (var c in input.EnumerateContent())
        {
            if (Predicate(c))
            {
                var (output, remainder) = input.SplitAtPosition(count);

                return Result.Create(remainder, output);
            }
            else
            {
                count = count + 1;
            }
        }

        throw new InvalidOperationException("Could not parse till predicate at head");
    }
}

public static class TakeTillRequired
{
    public static ITakeTillRequiredParser<T> Create<T>(TakeTillRequiredPredicate predicate)
        where T : IParsable, ISplitableAtPosition<T>, IContentEnumerable<char>, IEmptyCheckable
        => new TakeTillRequiredParser<T>(predicate);
}
