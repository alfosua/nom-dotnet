using Nom.Sdk;

namespace Nom.Strings;

public delegate bool TakeWhilePredicate(char value);

public interface ITakeWhileParser<T> : IParser<T, T>
    where T : IParsable, ISplitableAtPosition<T>, IContentEnumerable<char>
{
}

public class TakeWhileParser<T> : ITakeWhileParser<T>
    where T : IParsable, ISplitableAtPosition<T>, IContentEnumerable<char>
{
    public TakeWhileParser(TakeWhilePredicate predicate)
    {
        Predicate = predicate;
    }

    public TakeWhilePredicate Predicate { get; }

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
                break;
            }
        }

        var (output, remainder) = input.SplitAtPosition(count);

        return Result.Create(remainder, output);
    }
}

public static class TakeWhile
{
    public static ITakeWhileParser<T> Create<T>(TakeWhilePredicate predicate)
        where T : IParsable, ISplitableAtPosition<T>, IContentEnumerable<char>
        => new TakeWhileParser<T>(predicate);
}
