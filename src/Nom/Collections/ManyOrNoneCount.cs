namespace Nom.Collections;

public interface IManyOrNoneCountParser<TInput, TOutput> : IParser<TInput, int>
    where TInput : IParsable
{
}

public class ManyOrNoneCountParser<TInput, TOutput> : IManyOrNoneCountParser<TInput, TOutput>
    where TInput : IParsable
{
    public ManyOrNoneCountParser(IParser<TInput, TOutput> parser)
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

        return Result.Create(remainder, count);
    }
}

public static class ManyOrNoneCount
{
    public static IParser<TInput, int>
        Create<TInput, TOutput>(IParser<TInput, TOutput> parser)
            where TInput : IParsable
    {
        return new ManyOrNoneCountParser<TInput, TOutput>(parser);
    }
}
