namespace Nom.Combinators;

public interface IOptionalParser<TInput, TOutput> : IParser<TInput, TOutput>
    where TInput : IParsable
    where TOutput : new()
{
}

public class OptionalParser<TInput, TOutput> : IOptionalParser<TInput, TOutput>
    where TInput : IParsable
    where TOutput : new()
{
    public OptionalParser(IParser<TInput, TOutput> parser)
    {
        Parser = parser;
    }

    public IParser<TInput, TOutput> Parser { get; }

    public IResult<TInput, TOutput> Parse(TInput input)
    {
        try
        {
            var result = Parser.Parse(input);
            return result;
        }
        catch (InvalidOperationException)
        {
            var output = new TOutput();

            if (output is IRemainderMutator remainderMutator && input is IEmptyTailParsableDecorator decorator)
            {
                decorator.DecorateEmptyTail(remainderMutator);
            }

            return Result.Create(input, output);
        }
    }
}

public static class Optional
{
    public static IOptionalParser<TInput, TOutput> Create<TInput, TOutput>(IParser<TInput, TOutput> parser)
        where TInput : IParsable
        where TOutput : new()
    {
        return new OptionalParser<TInput, TOutput>(parser);
    }
}
