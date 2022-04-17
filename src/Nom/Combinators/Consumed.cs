namespace Nom.Combinators;

public interface IConsumedParser<TInput, TOutput> : IParser<TInput, (TInput, TOutput)>
    where TInput : IParsable
{
}

public class ConsumedParser<TInput, TOutput> : IConsumedParser<TInput, TOutput>
    where TInput : IParsable
{
    public ConsumedParser(IParser<TInput, TOutput> parser)
    {
        Parser = parser;
    }

    public IParser<TInput, TOutput> Parser { get; }

    public IResult<TInput, (TInput, TOutput)> Parse(TInput input)
    {
        var result = Parser.Parse(input);
        return Result.Create(result.Remainder, (input, result.Output));
    }
}

public static class Consumed
{
    public static IConsumedParser<TInput, TOutput> Create<TInput, TOutput>(IParser<TInput, TOutput> parser)
        where TInput : IParsable
    {
        return new ConsumedParser<TInput, TOutput>(parser);
    }
}
