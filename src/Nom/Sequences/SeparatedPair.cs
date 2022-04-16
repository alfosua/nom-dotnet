namespace Nom.Sequences;

public interface ISeparatedPairParser<TCommonInput, TLeftOutput, TSeparatorOutput, TRightOutput>
    : IParser<TCommonInput, (TLeftOutput, TRightOutput)>
{
}

public class SeparatedPairParser<TCommonInput, TLeftOutput, TSeparatorOutput, TRightOutput>
    : ISeparatedPairParser<TCommonInput, TLeftOutput, TSeparatorOutput, TRightOutput>
{
    public SeparatedPairParser(IParser<TCommonInput, TLeftOutput> left, IParser<TCommonInput, TSeparatorOutput> separator, IParser<TCommonInput, TRightOutput> right)
    {
        Left = left;
        Separator = separator;
        Right = right;
    }

    public IParser<TCommonInput, TLeftOutput> Left { get; }
    public IParser<TCommonInput, TSeparatorOutput> Separator { get; }
    public IParser<TCommonInput, TRightOutput> Right { get; }

    public IResult<TCommonInput, (TLeftOutput, TRightOutput)> Parse(TCommonInput input)
    {
        var leftResult = Left.Parse(input);
        var separtorResult = Separator.Parse(leftResult.Remaining);
        var rightResult = Right.Parse(separtorResult.Remaining);
        return Result.Create(rightResult.Remaining, (leftResult.Output, rightResult.Output));
    }
}

public static class SeparatedPair
{
    public static ISeparatedPairParser<TCommonInput, TLeftOutput, TSeparatorOutput, TRightOutput>
        Create<TCommonInput, TLeftOutput, TSeparatorOutput, TRightOutput>(
            IParser<TCommonInput, TLeftOutput> left,
            IParser<TCommonInput, TSeparatorOutput> separator,
            IParser<TCommonInput, TRightOutput> right)
    {
        return new SeparatedPairParser<TCommonInput, TLeftOutput, TSeparatorOutput, TRightOutput>(left, separator, right);
    }
}
