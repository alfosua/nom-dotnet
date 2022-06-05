namespace Nom.Collections;

public interface IManyOrNoneParser<TInput, TOutput> : IParser<TInput, ICollection<TOutput>>
    where TInput : IParsable
{
}

public class ManyOrNoneParser<TInput, TOutput> : IManyOrNoneParser<TInput, TOutput>
    where TInput : IParsable
{
    public ManyOrNoneParser(IParser<TInput, TOutput> parser)
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

        return Result.Create(remainder, outputs);
    }
}

public static class ManyOrNone
{
    public static IParser<TInput, ICollection<TOutput>>
        Create<TInput, TOutput>(IParser<TInput, TOutput> parser)
            where TInput : IParsable
    {
        return new ManyOrNoneParser<TInput, TOutput>(parser);
    }
}
