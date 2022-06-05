namespace Nom.Combinators;

public interface IValueParser<TInput, TParserOutput, TOutput> : IParser<TInput, TOutput>
    where TInput : IParsable
{
    TOutput Value { get; }
    IParser<TInput, TParserOutput> Parser { get; }
}

public class ValueParser<TInput, TParserOutput, TOutput> : IValueParser<TInput, TParserOutput, TOutput>
    where TInput : IParsable
{
    public ValueParser(TOutput value, IParser<TInput, TParserOutput> parser)
    {
        Value = value;
        Parser = parser;
    }

    public TOutput Value { get; }
    public IParser<TInput, TParserOutput> Parser { get; }

    public IResult<TInput, TOutput> Parse(TInput input)
    {
        var result = Parser.Parse(input);
        return Result.Create(result.Remainder, Value);
    }
}

public static class Value
{
    public static IParser<TInput, TOutput> Create<TInput, TParserOutput, TOutput>(
        TOutput value, IParser<TInput, TParserOutput> parser)
        where TInput : IParsable
    {
        return new ValueParser<TInput, TParserOutput, TOutput>(value, parser);
    }
}
