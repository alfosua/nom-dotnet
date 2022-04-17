namespace Nom.Combinators;

public interface ISuccessParser<TInput, TOutput> : IParser<TInput, TOutput>
    where TInput : IParsable
{
    TOutput Value { get; }
}

public class SuccessParser<TInput, TOutput> : ISuccessParser<TInput, TOutput>
    where TInput : IParsable
{
    public SuccessParser(TOutput value)
    {
        Value = value;
    }

    public TOutput Value { get; }

    public IResult<TInput, TOutput> Parse(TInput input)
    {
        return Result.Create(input, Value);
    }
}

public static class Success
{
    public static ISuccessParser<TInput, TOutput> Create<TInput, TOutput>(TOutput value)
        where TInput : IParsable
    {
        return new SuccessParser<TInput, TOutput>(value);
    }
}
