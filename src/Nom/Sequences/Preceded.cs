namespace Nom.Sequences;

public interface IPrecededParser<TCommonInput, TPrecedingOutput, TTargetOutput>
    : IParser<TCommonInput, TTargetOutput>
    where TCommonInput : IParsable
{
}

public class PrecededParser<TCommonInput, TPrecedingOutput, TTargetOutput>
    : IPrecededParser<TCommonInput, TPrecedingOutput, TTargetOutput>
    where TCommonInput : IParsable
{
    public PrecededParser(
        IParser<TCommonInput, TPrecedingOutput> precedingParser,
        IParser<TCommonInput, TTargetOutput> targetParser)
    {
        PrecedingParser = precedingParser;
        TargetParser = targetParser;
    }

    public IParser<TCommonInput, TPrecedingOutput> PrecedingParser { get; }
    public IParser<TCommonInput, TTargetOutput> TargetParser { get; }

    public IResult<TCommonInput, TTargetOutput> Parse(TCommonInput input)
    {
        var precedingResult = PrecedingParser.Parse(input);
        var targetResult = TargetParser.Parse(precedingResult.Remainder);
        return Result.Create(targetResult.Remainder, targetResult.Output);
    }
}

public static class Preceded
{

    public static IPrecededParser<TCommonInput, TPrecedingOutput, TTargetOutput>
        Create<TCommonInput, TPrecedingOutput, TTargetOutput>(
            IParser<TCommonInput, TPrecedingOutput> precedingParser,
            IParser<TCommonInput, TTargetOutput> targetParser)
            where TCommonInput : IParsable
    {
        return new PrecededParser<TCommonInput, TPrecedingOutput, TTargetOutput>(
            precedingParser, targetParser);
    }
}
