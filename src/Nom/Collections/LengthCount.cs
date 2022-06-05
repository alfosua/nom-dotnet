namespace Nom.Collections;

public interface ILengthCountParser<TInput, TCountOutput, TItemOutput> : IParser<TInput, ICollection<TItemOutput>>
    where TInput : IParsable
{
}

public class LengthCountParser<TInput, TCountOutput, TItemOutput> : ILengthCountParser<TInput, TCountOutput, TItemOutput>
    where TInput : IParsable
{
    public LengthCountParser(IParser<TInput, TCountOutput> countParser, IParser<TInput, TItemOutput> itemParser)
    {
        CountParser = countParser;
        ItemParser = itemParser;
    }

    public IParser<TInput, TCountOutput> CountParser { get; }
    public IParser<TInput, TItemOutput> ItemParser { get; }

    public IResult<TInput, ICollection<TItemOutput>> Parse(TInput input)
    {
        ICollection<TItemOutput> outputs = new List<TItemOutput>();
        var (remainder, countRaw) = CountParser.Parse(input);
        var count = Convert.ToUInt32(countRaw);

        for (uint i = 0; i < count; i++)
        {
            var result = ItemParser.Parse(remainder);
            remainder = result.Remainder;
            outputs.Add(result.Output);
        }

        return Result.Create(remainder, outputs);
    }
}

public static class LengthCount
{
    public static IParser<TInput, ICollection<TItemOutput>>
        Create<TInput, TCountOutput, TItemOutput>(
            IParser<TInput, TCountOutput> countParser, IParser<TInput, TItemOutput> itemParser)
            where TInput : IParsable
    {
        return new LengthCountParser<TInput, TCountOutput, TItemOutput>(countParser, itemParser);
    }
}
