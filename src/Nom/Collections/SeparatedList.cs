namespace Nom.Collections;

public interface ISeparatedListParser<TCommonInput, TSeparatorOutput, TEachItemOutput>
    : IParser<TCommonInput, ICollection<TEachItemOutput>>
    where TCommonInput : IParsable
{
}

public class SeparatedListParser<TCommonInput, TSeparatorOutput, TEachItemOutput>
    : ISeparatedListParser<TCommonInput, TSeparatorOutput, TEachItemOutput>
    where TCommonInput : IParsable
{
    public SeparatedListParser(IParser<TCommonInput, TSeparatorOutput> separator, IParser<TCommonInput, TEachItemOutput> eachItem)
    {
        EachItem = eachItem;
        Separator = separator;
    }

    public IParser<TCommonInput, TSeparatorOutput> Separator { get; }
    public IParser<TCommonInput, TEachItemOutput> EachItem { get; }

    public IResult<TCommonInput, ICollection<TEachItemOutput>> Parse(TCommonInput input)
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

public static class SeparatedList
{
    public static ISeparatedListParser<TCommonInput, TSeparatorOutput, TEachItemOutput>
        Create<TCommonInput, TSeparatorOutput, TEachItemOutput>(
            IParser<TCommonInput, TSeparatorOutput> separatorParser,
            IParser<TCommonInput, TEachItemOutput> eachItemParser)
            where TCommonInput : IParsable
    {
        return new SeparatedListParser<TCommonInput, TSeparatorOutput, TEachItemOutput>(separatorParser, eachItemParser);
    }
}
