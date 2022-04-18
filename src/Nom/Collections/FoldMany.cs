namespace Nom.Collections;

public delegate TAccumulation FoldManyAggregator<TAccumulation, TOutput>(TAccumulation accumulation, TOutput next);

public interface IFoldManyParser<TInput, TOutput, TAccumulation> : IParser<TInput, TAccumulation>
    where TInput : IParsable
{
}

public class FoldManyParser<TInput, TOutput, TAccumulation> : IFoldManyParser<TInput, TOutput, TAccumulation>
    where TInput : IParsable
{
    public FoldManyParser(IParser<TInput, TOutput> parser, TAccumulation initial, FoldManyAggregator<TAccumulation, TOutput> aggregator)
    {
        Parser = parser;
        Initial = initial;
        Aggregator = aggregator;
    }

    public IParser<TInput, TOutput> Parser { get; }
    public TAccumulation Initial { get; set; }
    public FoldManyAggregator<TAccumulation, TOutput> Aggregator { get; set; }

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
        catch(InvalidOperationException) { }

        if (count == 0)
        {
            throw new InvalidOperationException("Could not parse anything to fold with given criteria");
        }

        return Result.Create(remainder, accumulation);
    }
}

public static class FoldMany
{
    public static IFoldManyParser<TInput, TOutput, TAccumulation>
        Create<TInput, TOutput, TAccumulation>(
            IParser<TInput, TOutput> parser, TAccumulation initial, FoldManyAggregator<TAccumulation, TOutput> aggregator)
            where TInput : IParsable
    {
        return new FoldManyParser<TInput, TOutput, TAccumulation>(parser, initial, aggregator);
    }
}
