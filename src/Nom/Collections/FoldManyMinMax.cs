namespace Nom.Collections;

public delegate TAccumulation FoldManyMinMaxAggregator<TAccumulation, TOutput>(TAccumulation accumulation, TOutput next);

public interface IFoldManyMinMaxParser<TInput, TOutput, TAccumulation> : IParser<TInput, TAccumulation>
    where TInput : IParsable
{
}

public class FoldManyMinMaxParser<TInput, TOutput, TAccumulation> : IFoldManyMinMaxParser<TInput, TOutput, TAccumulation>
    where TInput : IParsable
{
    public FoldManyMinMaxParser(int min, int max, IParser<TInput, TOutput> parser, TAccumulation initial, FoldManyMinMaxAggregator<TAccumulation, TOutput> aggregator)
    {
        Min = min;
        Max = max;
        Parser = parser;
        Initial = initial;
        Aggregator = aggregator;
    }

    public int Min { get; set; }
    public int Max { get; set; }
    public IParser<TInput, TOutput> Parser { get; }
    public TAccumulation Initial { get; set; }
    public FoldManyMinMaxAggregator<TAccumulation, TOutput> Aggregator { get; set; }

    public IResult<TInput, TAccumulation> Parse(TInput input)
    {
        var count = 0;
        var accumulation = Initial;
        var remainder = input;

        try
        {
            while (count < Max)
            {
                var result = Parser.Parse(remainder);
                remainder = result.Remainder;
                accumulation = Aggregator.Invoke(accumulation, result.Output);
                count = count + 1;
            }
        }
        catch (InvalidOperationException) { }

        if (count == 0)
        {
            throw new InvalidOperationException("Could not parse anything to fold with given criteria");
        }
        else if (count < Min)
        {
            throw new InvalidOperationException("Could not suffice minimum matches to fold with given criteria");
        }

        return Result.Create(remainder, accumulation);
    }
}

public static class FoldManyMinMax
{
    public static IParser<TInput, TAccumulation>
        Create<TInput, TOutput, TAccumulation>(
            int min, int max, IParser<TInput, TOutput> parser, TAccumulation initial, FoldManyMinMaxAggregator<TAccumulation, TOutput> aggregator)
            where TInput : IParsable
    {
        return new FoldManyMinMaxParser<TInput, TOutput, TAccumulation>(min, max, parser, initial, aggregator);
    }
}
