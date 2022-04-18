using Nom.Sdk;

namespace Nom.Strings;

public delegate bool TakeWhileMinMaxPredicate(char value);

public interface ITakeWhileMinMaxParser<T> : IParser<T, T>
    where T : IParsable, ISplitableAtPosition<T>, IContentEnumerable<char>, IEmptyCheckable
{
}

public class TakeWhileMinMaxParser<T> : ITakeWhileMinMaxParser<T>
    where T : IParsable, ISplitableAtPosition<T>, IContentEnumerable<char>, IEmptyCheckable
{
    public TakeWhileMinMaxParser(TakeWhileMinMaxPredicate predicate, int min, int max)
    {
        Predicate = predicate;
        Min = min;
        Max = max;
    }

    public TakeWhileMinMaxPredicate Predicate { get; }
    public int Min { get; }
    public int Max { get; }

    public IResult<T, T> Parse(T input)
    {
        CommonParsings.CheckInputIsNotEmpty(input);

        int count = 0;

        foreach (var c in input.EnumerateContent())
        {
            if (count < Max && Predicate(c))
            {
                count = count + 1;
            }
            else if (count >= Min)
            {
                var (output, remainder) = input.SplitAtPosition(count);

                return Result.Create(remainder, output);
            }
            else
            {
                if (count > 0)
                {
                    throw new InvalidOperationException("Could not satisfy minimum matches with given predicate");
                }
                else
                {
                    throw new InvalidOperationException("Could not take anything with given predicate");
                }
            }
        }

        throw new InvalidOperationException("Could not take anything out of range");
    }
}

public static class TakeWhileMinMax
{
    public static ITakeWhileMinMaxParser<T> Create<T>(TakeWhileMinMaxPredicate predicate, int min, int max)
        where T : IParsable, ISplitableAtPosition<T>, IContentEnumerable<char>, IEmptyCheckable
        => new TakeWhileMinMaxParser<T>(predicate, min, max);
}
