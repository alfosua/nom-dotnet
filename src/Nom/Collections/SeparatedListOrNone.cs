namespace Nom.Collections;

public interface ISeparatedListOrNoneParser<TInput, TSeparatorOutput, TEachItemOutput>
    : IParser<TInput, ICollection<TEachItemOutput>>
    where TInput : IParsable
{
}

public class SeparatedListOrNoneParser<TInput, TSeparatorOutput, TEachItemOutput>
    : ISeparatedListOrNoneParser<TInput, TSeparatorOutput, TEachItemOutput>
    where TInput : IParsable
{
    public SeparatedListOrNoneParser(IParser<TInput, TSeparatorOutput> separator, IParser<TInput, TEachItemOutput> eachItem)
    {
        EachItem = eachItem;
        Separator = separator;
    }

    public IParser<TInput, TSeparatorOutput> Separator { get; }
    public IParser<TInput, TEachItemOutput> EachItem { get; }

    public IResult<TInput, ICollection<TEachItemOutput>> Parse(TInput input)
    {
        ICollection<TEachItemOutput> outputs = new List<TEachItemOutput>();
        var stillListing = true;
        var remaining = input;
        
        while (stillListing)
        {
            var itemResult = EachItem.Parse(remaining);
            remaining = itemResult.Remainder;
            outputs.Add(itemResult.Output);
            try
            {
                var separatorResult = Separator.Parse(itemResult.Remainder);
                remaining = separatorResult.Remainder;
            }
            catch
            {
                stillListing = false;

            }
        }
        return Result.Create(remaining, outputs);
    }
}

public static class SeparatedListOrNone
{
    public static ISeparatedListOrNoneParser<TInput, TSeparatorOutput, TEachItemOutput>
        Create<TInput, TSeparatorOutput, TEachItemOutput>(
            IParser<TInput, TSeparatorOutput> separatorParser,
            IParser<TInput, TEachItemOutput> eachItemParser)
            where TInput : IParsable
    {
        return new SeparatedListOrNoneParser<TInput, TSeparatorOutput, TEachItemOutput>(separatorParser, eachItemParser);
    }
}
