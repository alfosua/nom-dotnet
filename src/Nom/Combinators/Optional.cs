namespace Nom.Combinators;

public interface IOptionalParser<TInput, TOutput> : IParser<TInput, TOutput?>
    where TInput : IParsable
    where TOutput : class
{
}

public class OptionalParser<TInput, TOutput> : IOptionalParser<TInput, TOutput>
    where TInput : IParsable
    where TOutput : class
{
    public OptionalParser(IParser<TInput, TOutput> parser)
    {
        Parser = parser;
    }

    public IParser<TInput, TOutput> Parser { get; }

    public IResult<TInput, TOutput?> Parse(TInput input)
    {
        try
        {
            var result = Parser.Parse(input);
            return Result.Create<TInput, TOutput?>(result.Remainder, result.Output);
        }
        catch (InvalidOperationException)
        {
            return Result.Create(input, (TOutput?)null);
        }
    }
}

public static class Optional
{
    public static IOptionalParser<TInput, TOutput> Create<TInput, TOutput>(IParser<TInput, TOutput> parser)
        where TInput : IParsable
        where TOutput : class
    {
        return new OptionalParser<TInput, TOutput>(parser);
    }
}
