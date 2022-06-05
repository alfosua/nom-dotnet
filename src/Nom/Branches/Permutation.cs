namespace Nom.Branches;

public interface IPermutationParser<TCommonInput, TEachParserOutput>
    : IParser<TCommonInput, ICollection<TEachParserOutput>>
    where TCommonInput : IParsable
{
}

public class PermutationParser<TCommonInput, TEachParserOutput>
    : IPermutationParser<TCommonInput, TEachParserOutput>
    where TCommonInput : IParsable
{
    public PermutationParser(IEnumerable<IParser<TCommonInput, TEachParserOutput>> parsers)
    {
        Parsers = parsers;
    }
    
    public IEnumerable<IParser<TCommonInput, TEachParserOutput>> Parsers { get; }

    public IResult<TCommonInput, ICollection<TEachParserOutput>> Parse(TCommonInput input)
    {
        var lookup = Parsers
            .Select((x, i) => (x, i))
            .ToDictionary(t => t.i, t => t.x);

        var values = new SortedDictionary<int, TEachParserOutput>();
        var remainder = input;
        int? lastValuesCount = null;

        do
        {
            if (values.Count == lastValuesCount)
            {
                throw new InvalidOperationException($"No one parser matched after {values.Count} parsings");
            }
            
            foreach (var (index, parser) in lookup.Where(x => !values.ContainsKey(x.Key)))
            {
                try
                {
                    var result = parser.Parse(remainder);
                    lastValuesCount = values.Count;
                    values[index] = result.Output;
                    remainder = result.Remainder;
                }
                catch (InvalidOperationException) { }
            }
        } while (values.Count < lookup.Count);

        return Result.Create(remainder, values.Values as ICollection<TEachParserOutput>);
    }
}

public static class Permutation
{
    public static IParser<TCommonInput, ICollection<TEachParserOutput>>
        Create<TCommonInput, TEachParserOutput>(
            IEnumerable<IParser<TCommonInput, TEachParserOutput>> parsers)
            where TCommonInput : IParsable
    {
        return new PermutationParser<TCommonInput, TEachParserOutput>(parsers);
    }
    
    public static IParser<TCommonInput, ICollection<TEachParserOutput>>
        Create<TCommonInput, TEachParserOutput>(
            params IParser<TCommonInput, TEachParserOutput>[] parsers)
            where TCommonInput : IParsable
    {
        return new PermutationParser<TCommonInput, TEachParserOutput>(parsers);
    }
}
