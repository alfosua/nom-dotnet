namespace Nom.Sequences;

public interface ITerminatedParser<TCommonInput, TTerminatingOutput, TTargetOutput>
    : IParser<TCommonInput, TTargetOutput>
    where TCommonInput : IParsable
{
}

public class TerminatedParser<TCommonInput, TTerminatingOutput, TTargetOutput>
    : ITerminatedParser<TCommonInput, TTerminatingOutput, TTargetOutput>
    where TCommonInput : IParsable
{
    public TerminatedParser(
        IParser<TCommonInput, TTerminatingOutput> terminatingParser,
        IParser<TCommonInput, TTargetOutput> targetParser)
    {
        TerminatingParser = terminatingParser;
        TargetParser = targetParser;
    }

    public IParser<TCommonInput, TTerminatingOutput> TerminatingParser { get; }
    public IParser<TCommonInput, TTargetOutput> TargetParser { get; }

    public IResult<TCommonInput, TTargetOutput> Parse(TCommonInput input)
    {
        var terminatingResult = TerminatingParser.Parse(input);
        var targetResult = TargetParser.Parse(terminatingResult.Remainder);
        return Result.Create(targetResult.Remainder, targetResult.Output);
    }
}

public static class Terminated
{

    public static ITerminatedParser<TCommonInput, TTerminatingOutput, TTargetOutput>
        Create<TCommonInput, TTerminatingOutput, TTargetOutput>(
            IParser<TCommonInput, TTerminatingOutput> terminatingParser,
            IParser<TCommonInput, TTargetOutput> targetParser)
            where TCommonInput : IParsable
    {
        return new TerminatedParser<TCommonInput, TTerminatingOutput, TTargetOutput>(
            terminatingParser, targetParser);
    }
}
