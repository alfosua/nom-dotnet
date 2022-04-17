namespace Nom.Combinators;

public interface INotParser<TInput, TOutput> : IParser<TInput, TOutput>
    where TInput : IParsable
    where TOutput : new()
{
}

public class NotParser<TInput, TOutput> : INotParser<TInput, TOutput>
    where TInput : IParsable
    where TOutput : new()
{
    public NotParser(IParser<TInput, TOutput> parser)
    {
        Parser = parser;
    }

    public IParser<TInput, TOutput> Parser { get; }

    public IResult<TInput, TOutput> Parse(TInput input)
    {
        try
        {
            var result = Parser.Parse(input);
            throw new InvalidOperationException("Should not have parsed");
        }
        catch
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

public static class Not
{
    public static INotParser<TInput, TOutput> Create<TInput, TOutput>(IParser<TInput, TOutput> parser)
        where TInput : IParsable
        where TOutput : new()
    {
        return new NotParser<TInput, TOutput>(parser);
    }
}
