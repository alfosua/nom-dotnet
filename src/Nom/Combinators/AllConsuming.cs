namespace Nom.Combinators;

public interface IAllConsumingParser<TInput, TOutput> : IParser<TInput, TOutput>
    where TInput : IParsable, IEmptyCheckable
{
}

public class AllConsumingParser<TInput, TOutput> : IAllConsumingParser<TInput, TOutput>
    where TInput : IParsable, IEmptyCheckable
{
    public AllConsumingParser(IParser<TInput, TOutput> parser)
    {
        Parser = parser;
    }

    public IParser<TInput, TOutput> Parser { get; }

    public IResult<TInput, TOutput> Parse(TInput input)
    {
        var result = Parser.Parse(input);

        if (result.Remainder.IsEmpty())
        {
            return Result.Create(input, result.Output);
        }
        else
        {
            throw new InvalidOperationException("Parser did not consume all input");   
        }
    }
}

public static class AllConsuming
{
    public static IParser<TInput, TOutput>
        Create<TInput, TOutput>(IParser<TInput, TOutput> parser)
        where TInput : IParsable, IEmptyCheckable
    {
        return new AllConsumingParser<TInput, TOutput>(parser);
    }
}
