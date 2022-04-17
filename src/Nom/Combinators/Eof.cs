namespace Nom.Combinators;

public interface IEofParser<TInput, TOutput> : IParser<TInput, TOutput>
    where TInput : IParsable
    where TOutput : new()
{
}

public class EofParser<TInput, TOutput> : IEofParser<TInput, TOutput>
    where TInput : IParsable
    where TOutput : new()
{
    public EofParser(IParser<TInput, TOutput> parser)
    {
        Parser = parser;
    }

    public IParser<TInput, TOutput> Parser { get; }

    public IResult<TInput, TOutput> Parse(TInput input)
    {
        if (input.IsEmpty())
        {
            var output = new TOutput();

            if (output is IRemainderMutator remainderMutator && input is IEmptyTailParsableDecorator decorator)
            {
                decorator.DecorateEmptyTail(remainderMutator);
            }

            return Result.Create(input, output);
        }
        else
        {
            throw new InvalidOperationException("Could not parse end of input data");
        }
    }
}

public static class Eof
{
    public static IEofParser<TInput, TOutput> Create<TInput, TOutput>(IParser<TInput, TOutput> parser)
        where TInput : IParsable
        where TOutput : new()
    {
        return new EofParser<TInput, TOutput>(parser);
    }
}
