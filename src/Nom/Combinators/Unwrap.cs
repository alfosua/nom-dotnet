namespace Nom.Combinators;

public interface IUnwrapParser<TInput, TParserOutput, TOutput> : IParser<TInput, TOutput>
    where TInput : IParsable
    where TParserOutput : IContentVisitable<TOutput>
{
}

public class UnwrapParser<TInput, TParserOutput, TOutput> : IUnwrapParser<TInput, TParserOutput, TOutput>
    where TInput : IParsable
    where TParserOutput : IContentVisitable<TOutput>
{
    public UnwrapParser(IParser<TInput, TParserOutput> parser)
    {
        Parser = parser;
    }

    public IParser<TInput, TParserOutput> Parser { get; }

    public IResult<TInput, TOutput> Parse(TInput input)
    {
        var result = Parser.Parse(input);
        return Result.Create(input, result.Output.VisitContent());
    }
}

public static class Unwrap
{
    public static IUnwrapParser<TInput, TParserOutput, TOutput>
        Create<TInput, TParserOutput, TOutput>(
            IParser<TInput, TParserOutput> parser)
            where TInput : IParsable
            where TParserOutput : IContentVisitable<TOutput>
    {
        return new UnwrapParser<TInput, TParserOutput, TOutput>(parser);
    }
}
