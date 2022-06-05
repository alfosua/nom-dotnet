namespace Nom.Combinators;

public interface IConditionalParser<TInput, TOutput> : IParser<TInput, TOutput>
    where TInput : IParsable
    where TOutput : new()
{
}

public class ConditionalParser<TInput, TOutput> : IConditionalParser<TInput, TOutput>
    where TInput : IParsable
    where TOutput : new()
{
    public ConditionalParser(bool condition, IParser<TInput, TOutput> parser)
    {
        Condition = condition;
        Parser = parser;
    }

    public bool Condition { get; }
    public IParser<TInput, TOutput> Parser { get; }

    public IResult<TInput, TOutput> Parse(TInput input)
    {
        if (Condition)
        {
            return Parser.Parse(input);
        }
        else
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

public static class Conditional
{
    public static IParser<TInput, TOutput> Create<TInput, TOutput>(
        bool condition,
        IParser<TInput, TOutput> parser)
        where TInput : IParsable
        where TOutput : new()
    {
        return new ConditionalParser<TInput, TOutput>(condition, parser);
    }
}
