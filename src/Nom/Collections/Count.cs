namespace Nom.Collections;

public interface ICountParser<TInput, TOutput> : IParser<TInput, ICollection<TOutput>>
    where TInput : IParsable
{
}

public class CountParser<TInput, TOutput> : ICountParser<TInput, TOutput>
    where TInput : IParsable
{
    public CountParser(IParser<TInput, TOutput> parser, int count)
    {
        Parser = parser;
        Count = count;
    }

    public IParser<TInput, TOutput> Parser { get; }
    public int Count { get; set; }

    public IResult<TInput, ICollection<TOutput>> Parse(TInput input)
    {
        ICollection<TOutput> outputs = new List<TOutput>();

        var remainder = input;
        for (int i = 0; i < Count; i++)
        {
            var result = Parser.Parse(remainder);
            remainder = result.Remainder;
            outputs.Add(result.Output);
        }

        return Result.Create(remainder, outputs);
    }
}

public static class Count
{
    public static IParser<TInput, ICollection<TOutput>>
        Create<TInput, TOutput>(IParser<TInput, TOutput> parser, int count)
            where TInput : IParsable
    {
        return new CountParser<TInput, TOutput>(parser, count);
    }
}
