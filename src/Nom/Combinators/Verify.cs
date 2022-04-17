namespace Nom.Combinators;

public delegate bool VerifyParserPredicate<TOutput>(TOutput output);

public interface IVerifyParser<TInput, TOutput> : IParser<TInput, TOutput>
    where TInput : IParsable
{
    IParser<TInput, TOutput> Parser { get; }
    VerifyParserPredicate<TOutput> Predicate { get; set; }
}

public class VerifyParser<TInput, TOutput> : IVerifyParser<TInput, TOutput>
    where TInput : IParsable
{
    public VerifyParser(IParser<TInput, TOutput> parser, VerifyParserPredicate<TOutput> predicate)
    {
        Parser = parser;
        Predicate = predicate;
    }

    public IParser<TInput, TOutput> Parser { get; }
    public VerifyParserPredicate<TOutput> Predicate { get; set; }

    public IResult<TInput, TOutput> Parse(TInput input)
    {
        var result = Parser.Parse(input);

        if (Predicate(result.Output))
        {
            return result;
        }
        else
        {
            throw new Exception("Could not verify parser");
        }
    }
}

public static class Verify
{
    public static IVerifyParser<TInput, TOutput> Create<TInput, TOutput>(
        IParser<TInput, TOutput> parser,
        VerifyParserPredicate<TOutput> predicate)
        where TInput : IParsable
    {
        return new VerifyParser<TInput, TOutput>(parser, predicate);
    }
}
