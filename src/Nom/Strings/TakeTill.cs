using Nom.Sdk;

namespace Nom.Strings;

public delegate bool TakeTillPredicate(char value);

public interface ITakeTillParser<T> : IParser<T, T>
    where T : IParsable, ISplitableAtPosition<T>, IContentEnumerable<char>, IEmptyCheckable
{
}

public class TakeTillParser<T> : ITakeTillParser<T>
    where T : IParsable, ISplitableAtPosition<T>, IContentEnumerable<char>, IEmptyCheckable
{
    public TakeTillParser(TakeTillPredicate predicate)
    {
        Predicate = predicate;
    }

    public TakeTillPredicate Predicate { get; }

    public IResult<T, T> Parse(T input)
    {
        CommonParsings.CheckInputIsNotEmpty(input);
        
        int count = 0;

        foreach (var c in input.EnumerateContent())
        {
            if (Predicate(c))
            {
                break;
            }
            else
            {
                count = count + 1;
            }
        }

        var (output, remainder) = input.SplitAtPosition(count);

        return Result.Create(remainder, output);
    }
}

public static class TakeTill
{
    public static IParser<T, T> Create<T>(TakeTillPredicate predicate)
        where T : IParsable, ISplitableAtPosition<T>, IContentEnumerable<char>, IEmptyCheckable
        => new TakeTillParser<T>(predicate);
}
