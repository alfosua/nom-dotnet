namespace Nom.Collections;

public interface IManyTillParser<TInput, TItemOutput, TEndOutput> : IParser<TInput, (ICollection<TItemOutput>, TEndOutput)>
    where TInput : IParsable
{
}

public class ManyTillParser<TInput, TItemOutput, TEndOutput> : IManyTillParser<TInput, TItemOutput, TEndOutput>
    where TInput : IParsable
{
    public ManyTillParser(IParser<TInput, TItemOutput> itemParser, IParser<TInput, TEndOutput> endParser)
    {
        ItemParser = itemParser;
        EndParser = endParser;
    }

    public IParser<TInput, TItemOutput> ItemParser { get; }
    public IParser<TInput, TEndOutput> EndParser { get; }

    public IResult<TInput, (ICollection<TItemOutput>, TEndOutput)> Parse(TInput input)
    {
        ICollection<TItemOutput> outputs = new List<TItemOutput>();
        var remainder = input;

        while (true)
        {
            var itemResult = ItemParser.Parse(remainder);
            remainder = itemResult.Remainder;
            outputs.Add(itemResult.Output);
                
            try
            {
                var endResult = EndParser.Parse(remainder);
                remainder = endResult.Remainder;

                return Result.Create(remainder, (outputs, endResult.Output));
            }
            catch(InvalidOperationException) { }
        }
    }
}

public static class ManyTill
{
    public static IManyTillParser<TInput, TItemOutput, TEndOutput>
        Create<TInput, TItemOutput, TEndOutput>(
            IParser<TInput, TItemOutput> itemParser, IParser<TInput, TEndOutput> endParser)
            where TInput : IParsable
    {
        return new ManyTillParser<TInput, TItemOutput, TEndOutput>(itemParser, endParser);
    }
}
