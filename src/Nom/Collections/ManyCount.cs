namespace Nom.Collections;

public interface IManyCountParser<TInput, TOutput> : IParser<TInput, int>
    where TInput : IParsable
{
}

public class ManyCountParser<TInput, TOutput> : IManyCountParser<TInput, TOutput>
    where TInput : IParsable
{
    public ManyCountParser(IParser<TInput, TOutput> parser)
    {
        Parser = parser;
    }

    public IParser<TInput, TOutput> Parser { get; }

    public IResult<TInput, int> Parse(TInput input)
    {
        var count = 0;
        var remainder = input;

        try
        {
            while (true)
            {
                var result = Parser.Parse(remainder);
                remainder = result.Remainder;
                count = count + 1;
            }
        }
        catch(InvalidOperationException) { }

        if (count == 0)
        {
            throw new InvalidOperationException("Could not parse anything with given criteria");
        }

        return Result.Create(remainder, count);
    }
}

public static class ManyCount
{
    public static IManyCountParser<TInput, TOutput>
        Create<TInput, TOutput>(IParser<TInput, TOutput> parser)
            where TInput : IParsable
    {
        return new ManyCountParser<TInput, TOutput>(parser);
    }
}
