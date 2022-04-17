namespace Nom.Branches;

public interface IAlternationParser<TCommonInput, TEachParserOutput>
    : IParser<TCommonInput, TEachParserOutput>
    where TCommonInput : IParsable
{
}

public class AlternationParser<TCommonInput, TEachParserOutput>
    : IAlternationParser<TCommonInput, TEachParserOutput>
    where TCommonInput : IParsable
{
    public AlternationParser(IEnumerable<IParser<TCommonInput, TEachParserOutput>> parsers)
    {
        Parsers = parsers;
    }

    public IEnumerable<IParser<TCommonInput, TEachParserOutput>> Parsers { get; }

    public IResult<TCommonInput, TEachParserOutput> Parse(TCommonInput input)
    {
        foreach (var parser in Parsers)
        {
            try
            {
                var result = parser.Parse(input);
                return result;
            }
            catch (InvalidOperationException) { }
        }
        
        throw new InvalidOperationException("No one parser matched");
    }
}

public static class Alternation
{

    public static IAlternationParser<TCommonInput, TEachParserOutput>
        Create<TCommonInput, TEachParserOutput>(
            IEnumerable<IParser<TCommonInput, TEachParserOutput>> parsers)
            where TCommonInput : IParsable
    {
        return new AlternationParser<TCommonInput, TEachParserOutput>(parsers);
    }
}
