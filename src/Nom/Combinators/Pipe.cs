namespace Nom.Combinators;

public interface IPipeParser<TInput, TLeftParserOutput, TRightParserOutput> : IParser<TInput, TRightParserOutput>
    where TInput : IParsable
    where TLeftParserOutput : IParsable
{
}

public class PipeParser<TInput, TLeftParserOutput, TRightParserOutput>
    : IPipeParser<TInput, TLeftParserOutput, TRightParserOutput>
    where TInput : IParsable
    where TLeftParserOutput : IParsable
{
    public PipeParser(IParser<TInput, TLeftParserOutput> leftParser, IParser<TLeftParserOutput, TRightParserOutput> rightParser)
    {
        LeftParser = leftParser;
        RightParser = rightParser;
    }

    public IParser<TInput, TLeftParserOutput> LeftParser { get; set; }
    public IParser<TLeftParserOutput, TRightParserOutput> RightParser { get; set; }

    public IResult<TInput, TRightParserOutput> Parse(TInput input)
    {
        var leftResult = LeftParser.Parse(input);
        var rightResult = RightParser.Parse(leftResult.Output);
        return Result.Create(leftResult.Remainder, rightResult.Output);
    }
}

public static class Pipe
{
    public static IParser<TInput, TRightParserOutput>
        Create<TInput, TLeftParserOutput, TRightParserOutput>(
            IParser<TInput, TLeftParserOutput> leftParser,
            IParser<TLeftParserOutput, TRightParserOutput> rightParser)
            where TInput : IParsable
            where TLeftParserOutput : IParsable
    {
        return new PipeParser<TInput, TLeftParserOutput, TRightParserOutput>(leftParser, rightParser);
    }
}
