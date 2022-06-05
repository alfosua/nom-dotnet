namespace Nom.Collections;

public interface IManyMinMaxParser<TInput, TOutput> : IParser<TInput, ICollection<TOutput>>
    where TInput : IParsable
{
}

public class ManyMinMaxParser<TInput, TOutput> : IManyMinMaxParser<TInput, TOutput>
    where TInput : IParsable
{
    public ManyMinMaxParser(int min, int max, IParser<TInput, TOutput> parser)
    {
        Min = min;
        Max = max;
        Parser = parser;
    }

    public int Min { get; set; }
    public int Max { get; set; }
    public IParser<TInput, TOutput> Parser { get; }

    public IResult<TInput, ICollection<TOutput>> Parse(TInput input)
    {
        ICollection<TOutput> outputs = new List<TOutput>();
        var remainder = input;

        try
        {
            while (outputs.Count < Max)
            {
                var result = Parser.Parse(remainder);
                remainder = result.Remainder;
                outputs.Add(result.Output);
            }
        }
        catch(InvalidOperationException) { }

        if (outputs.Count == 0)
        {
            throw new InvalidOperationException("Could not parse anything with given criteria");
        }

        if (outputs.Count < Min)
        {
            throw new InvalidOperationException("Could not suffice minimum matches with given criteria");
        }

        return Result.Create(remainder, outputs);
    }
}

public static class ManyMinMax
{
    public static IParser<TInput, ICollection<TOutput>>
        Create<TInput, TOutput>(int min, int max, IParser<TInput, TOutput> parser)
            where TInput : IParsable
    {
        return new ManyMinMaxParser<TInput, TOutput>(min, max, parser);
    }
}
