namespace Nom.Combinators;

public interface IEofParser<TInput, TOutput> : IParser<TInput, TOutput>
    where TInput : IParsable, IEmptyCheckable
    where TOutput : new()
{
}

public class EofParser<TInput, TOutput> : IEofParser<TInput, TOutput>
    where TInput : IParsable, IEmptyCheckable
    where TOutput : new()
{
    public EofParser()
    {
    }

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
    public static IParser<StringParsable, StringParsable> Create()
    {
        return new EofParser<StringParsable, StringParsable>();
    }

    public static IParser<TInput, TOutput> Create<TInput, TOutput>()
        where TInput : IParsable, IEmptyCheckable
        where TOutput : new()
    {
        return new EofParser<TInput, TOutput>();
    }
}
