namespace Nom.Sequences;

public interface IDelimitedParser<TCommonInput, TLeftLimitOutput, TTargetOutput, TRightLimitOutput>
    : IParser<TCommonInput, TTargetOutput>
    where TCommonInput : IParsable
{
}

public class DelimitedParser<TCommonInput, TLeftLimitOutput, TTargetOutput, TRightLimitOutput>
    : IDelimitedParser<TCommonInput, TLeftLimitOutput, TTargetOutput, TRightLimitOutput>
    where TCommonInput : IParsable
{
    public DelimitedParser(
        IParser<TCommonInput, TLeftLimitOutput> leftLimitParser,
        IParser<TCommonInput, TTargetOutput> targetParser,
        IParser<TCommonInput, TRightLimitOutput> rightLimitParser)
    {
        LeftLimitParser = leftLimitParser;
        TargetParser = targetParser;
        RightLimitParser = rightLimitParser;
    }

    public IParser<TCommonInput, TLeftLimitOutput> LeftLimitParser { get; }
    public IParser<TCommonInput, TTargetOutput> TargetParser { get; }
    public IParser<TCommonInput, TRightLimitOutput> RightLimitParser { get; }

    public IResult<TCommonInput, TTargetOutput> Parse(TCommonInput input)
    {
        var leftLimitResult = LeftLimitParser.Parse(input);
        var targetResult = TargetParser.Parse(leftLimitResult.Remainder);
        var rightLimitResult = RightLimitParser.Parse(targetResult.Remainder);
        return Result.Create(rightLimitResult.Remainder, targetResult.Output);
    }
}

public static class Delimited
{

    public static IParser<TCommonInput, TTargetOutput>
        Create<TCommonInput, TLeftLimitOutput, TTargetOutput, TRightLimitOutput>(
            IParser<TCommonInput, TLeftLimitOutput> leftLimitParser,
            IParser<TCommonInput, TTargetOutput> targetParser,
            IParser<TCommonInput, TRightLimitOutput> rightLimitParser)
            where TCommonInput : IParsable
    {
        return new DelimitedParser<TCommonInput, TLeftLimitOutput, TTargetOutput, TRightLimitOutput>(
            leftLimitParser, targetParser, rightLimitParser);
    }
}
