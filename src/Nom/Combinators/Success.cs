namespace Nom.Combinators;

public interface ISuccessParser<TInput> : IParser<TInput, object?>
    where TInput : IParsable
{
}

public class SuccessParser<TInput> : ISuccessParser<TInput>
    where TInput : IParsable
{
    public IResult<TInput, object?> Parse(TInput input)
    {
        return Result.Create(input, (object?)null);
    }
}

public static class Success
{
    public static IParser<TInput, object?> Create<TInput>()
    where TInput : IParsable
    {
        return new SuccessParser<TInput>();
    }
}
