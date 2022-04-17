namespace Nom.Combinators;

public interface IRecognizeParser<TInput, TOutput> : IParser<TInput, TInput>
    where TInput : IParsable
{
}

public class RecognizeParser<TInput, TOutput> : IRecognizeParser<TInput, TOutput>
    where TInput : IParsable
{
    public RecognizeParser(IParser<TInput, TOutput> parser)
    {
        Parser = parser;
    }

    public IParser<TInput, TOutput> Parser { get; }

    public IResult<TInput, TInput> Parse(TInput input)
    {
        if (input is null)
        {
            throw new ArgumentNullException(nameof(input));
        }
        
        var result = Parser.Parse(input);
        return Result.Create(result.Remainder, input);
    }
}

public static class Recognize
{
    public static IRecognizeParser<TInput, TOutput> Create<TInput, TOutput>(IParser<TInput, TOutput> parser)
        where TInput : IParsable
    {
        return new RecognizeParser<TInput, TOutput>(parser);
    }
}
