namespace Nom.Sequences;

public interface ISequenceParser<TCommonInput, TEachParserOutput>
    : IParser<TCommonInput, ICollection<TEachParserOutput>>
    where TCommonInput : IParsable
{
}

public class SequenceParser<TCommonInput, TEachParserOutput>
    : ISequenceParser<TCommonInput, TEachParserOutput>
    where TCommonInput : IParsable
{
    public SequenceParser(IEnumerable<IParser<TCommonInput, TEachParserOutput>> parsers)
    {
        Parsers = parsers;
    }

    public IEnumerable<IParser<TCommonInput, TEachParserOutput>> Parsers { get; }

    public IResult<TCommonInput, ICollection<TEachParserOutput>> Parse(TCommonInput input)
    {
        var outputs = new List<TEachParserOutput>() as ICollection<TEachParserOutput>;
        var remainder = input;

        foreach (var parser in Parsers)
        {
            var result = parser.Parse(remainder);
            remainder = result.Remainder;
            outputs.Add(result.Output);
        }
        
        return Result.Create(remainder, outputs);
    }
}

public static class Sequence
{
    public static IParser<TCommonInput, ICollection<TEachParserOutput>>
        Create<TCommonInput, TEachParserOutput>(
            IEnumerable<IParser<TCommonInput, TEachParserOutput>> parsers)
            where TCommonInput : IParsable
    {
        return new SequenceParser<TCommonInput, TEachParserOutput>(parsers);
    }

    public static IParser<TCommonInput, ICollection<TEachParserOutput>>
        Create<TCommonInput, TEachParserOutput>(
            params IParser<TCommonInput, TEachParserOutput>[] parsers)
            where TCommonInput : IParsable
    {
        return new SequenceParser<TCommonInput, TEachParserOutput>(parsers);
    }
}
