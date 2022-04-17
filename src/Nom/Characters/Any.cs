using Nom.Sdk;

namespace Nom.Characters;

public interface IAnyParser<T> : IParser<T, T>
    where T : IParsable, ISplitableAtPosition<T>
{
}

public class AnyParser<T> : IAnyParser<T>
    where T : IParsable, ISplitableAtPosition<T>
{
    public IResult<T, T> Parse(T input)
    {
        CommonParsings.CheckInputIsNotEmpty(input);

        var (output, remainder) = input.SplitAtPosition(1);

        return Result.Create(remainder, output);
    }
}

public static class Any
{
    public static IAnyParser<StringParsable> Create() => new AnyParser<StringParsable>();

    public static IAnyParser<T> Create<T>()
        where T : IParsable, ISplitableAtPosition<T>
        => new AnyParser<T>();
}
