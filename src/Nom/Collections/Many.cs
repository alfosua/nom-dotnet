namespace Nom.Collections;

public interface IManyParser<TInput, TOutput> : IParser<TInput, ICollection<TOutput>>
    where TInput : IParsable
{
}

public class ManyParser<TInput, TOutput> : IManyParser<TInput, TOutput>
    where TInput : IParsable
{
    public ManyParser(IParser<TInput, TOutput> parser)
    {
        Parser = parser;
    }

    public IParser<TInput, TOutput> Parser { get; }

    public IResult<TInput, ICollection<TOutput>> Parse(TInput input)
    {
        ICollection<TOutput> outputs = new List<TOutput>();
        var remainder = input;

        try
        {
            while (true)
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

        return Result.Create(remainder, outputs);
    }
}

public static class Many
{
    public static IParser<TInput, ICollection<TOutput>>
        Create<TInput, TOutput>(IParser<TInput, TOutput> parser)
            where TInput : IParsable
    {
        return new ManyParser<TInput, TOutput>(parser);
    }
}
