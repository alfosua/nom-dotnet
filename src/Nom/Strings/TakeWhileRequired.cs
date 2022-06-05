using Nom.Sdk;

namespace Nom.Strings;

public delegate bool TakeWhileRequiredPredicate(char value);

public interface ITakeWhileRequiredParser<T> : IParser<T, T>
    where T : IParsable, ISplitableAtPosition<T>, IContentEnumerable<char>, IEmptyCheckable
{
}

public class TakeWhileRequiredParser<T> : ITakeWhileRequiredParser<T>
    where T : IParsable, ISplitableAtPosition<T>, IContentEnumerable<char>, IEmptyCheckable
{
    public TakeWhileRequiredParser(TakeWhileRequiredPredicate predicate)
    {
        Predicate = predicate;
    }

    public TakeWhileRequiredPredicate Predicate { get; }

    public IResult<T, T> Parse(T input)
    {
        CommonParsings.CheckInputIsNotEmpty(input);
        
        int count = 0;

        foreach (var c in input.EnumerateContent())
        {
            if (Predicate(c))
            {
                count = count + 1;
            }
            else
            {
                if (count == 0)
                {
                    throw new InvalidOperationException("Could not take anything with given predicate");
                }
                
                break;
            }
        }

        var (output, remainder) = input.SplitAtPosition(count);

        return Result.Create(remainder, output);
    }
}

public static class TakeWhileRequired
{
    public static IParser<T, T> Create<T>(TakeWhileRequiredPredicate predicate)
        where T : IParsable, ISplitableAtPosition<T>, IContentEnumerable<char>, IEmptyCheckable
        => new TakeWhileRequiredParser<T>(predicate);
}
