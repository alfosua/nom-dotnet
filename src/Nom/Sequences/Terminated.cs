namespace Nom.Sequences;

public interface ITerminatedParser<TCommonInput, TTargetOutput, TTerminatingOutput>
    : IParser<TCommonInput, TTargetOutput>
    where TCommonInput : IParsable
{
}

public class TerminatedParser<TCommonInput, TTargetOutput, TTerminatingOutput>
    : ITerminatedParser<TCommonInput, TTargetOutput, TTerminatingOutput>
    where TCommonInput : IParsable
{
    public TerminatedParser(
        IParser<TCommonInput, TTargetOutput> targetParser,
        IParser<TCommonInput, TTerminatingOutput> terminatingParser)
    {
        TargetParser = targetParser;
        TerminatingParser = terminatingParser;
    }

    public IParser<TCommonInput, TTargetOutput> TargetParser { get; }
    public IParser<TCommonInput, TTerminatingOutput> TerminatingParser { get; }

    public IResult<TCommonInput, TTargetOutput> Parse(TCommonInput input)
    {
        var targetResult = TargetParser.Parse(input);
        var terminatingResult = TerminatingParser.Parse(targetResult.Remainder);
        return Result.Create(terminatingResult.Remainder, targetResult.Output);
    }
}

public static class Terminated
{

    public static IParser<TCommonInput, TTargetOutput>
        Create<TCommonInput, TTargetOutput, TTerminatingOutput>(
            IParser<TCommonInput, TTargetOutput> targetParser,
            IParser<TCommonInput, TTerminatingOutput> terminatingParser)
            where TCommonInput : IParsable
    {
        return new TerminatedParser<TCommonInput, TTargetOutput, TTerminatingOutput>(targetParser, terminatingParser);
    }
}
