namespace Nom.Combinators;

public interface IPeekParser<TInput, TOutput> : IParser<TInput, TOutput>
    where TInput : IParsable
{
}

public class PeekParser<TInput, TOutput> : IPeekParser<TInput, TOutput>
    where TInput : IParsable
{
    public PeekParser(IParser<TInput, TOutput> parser)
    {
        Parser = parser;
    }

    public IParser<TInput, TOutput> Parser { get; }

    public IResult<TInput, TOutput> Parse(TInput input)
    {
        var result = Parser.Parse(input);
        return Result.Create(input, result.Output);
    }
}

public static class Peek
{
    public static IPeekParser<TInput, TOutput> Create<TInput, TOutput>(IParser<TInput, TOutput> parser)
        where TInput : IParsable
    {
        return new PeekParser<TInput, TOutput>(parser);
    }
}
