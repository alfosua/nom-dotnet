namespace Nom.Collections;

public interface IFoldManyOrNoneParser<TInput, TOutput, TAccumulation> : IParser<TInput, TAccumulation>
    where TInput : IParsable
{
}

public class FoldManyOrNoneParser<TInput, TOutput, TAccumulation> : IFoldManyOrNoneParser<TInput, TOutput, TAccumulation>
    where TInput : IParsable
{
    public FoldManyOrNoneParser(IParser<TInput, TOutput> parser, TAccumulation initial, Func<TAccumulation, TOutput, TAccumulation> aggregator)
    {
        Parser = parser;
        Initial = initial;
        Aggregator = aggregator;
    }

    public IParser<TInput, TOutput> Parser { get; }
    public TAccumulation Initial { get; set; }
    public Func<TAccumulation, TOutput, TAccumulation> Aggregator { get; set; }

    public IResult<TInput, TAccumulation> Parse(TInput input)
    {
        var count = 0;
        var accumulation = Initial;
        var remainder = input;

        try
        {
            while (true)
            {
                var result = Parser.Parse(remainder);
                remainder = result.Remainder;
                accumulation = Aggregator.Invoke(accumulation, result.Output);
                count = count + 1;
            }
        }
        catch (InvalidOperationException) { }

        return Result.Create(remainder, accumulation);
    }
}

public static class FoldManyOrNone
{
    public static IParser<TInput, TAccumulation>
        Create<TInput, TOutput, TAccumulation>(
            IParser<TInput, TOutput> parser, TAccumulation initial, Func<TAccumulation, TOutput, TAccumulation> aggregator)
            where TInput : IParsable
    {
        return new FoldManyOrNoneParser<TInput, TOutput, TAccumulation>(parser, initial, aggregator);
    }
}
