using Nom.Sdk;

namespace Nom.Strings;

public interface ITakeParser<T> : IParser<T, T>
    where T : IParsable, ISplitableAtPosition<T>, IEmptyCheckable
{
}

public class TakeParser<T> : ITakeParser<T>
    where T : IParsable, ISplitableAtPosition<T>, IEmptyCheckable
{
    public TakeParser(int count)
    {
        Count = count;
    }

    public int Count { get; }

    public IResult<T, T> Parse(T input)
    {
        CommonParsings.CheckInputIsNotEmpty(input);

        var (output, remainder) = input.SplitAtPosition(Count);

        return Result.Create(remainder, output);
    }
}

public static class Take
{
    public static IParser<T, T> Create<T>(int count)
        where T : IParsable, ISplitableAtPosition<T>, IEmptyCheckable
        => new TakeParser<T>(count);
}
