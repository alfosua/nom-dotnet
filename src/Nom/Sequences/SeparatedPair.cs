namespace Nom.Sequences;

public interface ISeparatedPairParser<TCommonInput, TLeftOutput, TSeparatorOutput, TRightOutput>
    : IParser<TCommonInput, (TLeftOutput, TRightOutput)>
    where TCommonInput : IParsable
{
}

public class SeparatedPairParser<TCommonInput, TLeftOutput, TSeparatorOutput, TRightOutput>
    : ISeparatedPairParser<TCommonInput, TLeftOutput, TSeparatorOutput, TRightOutput>
    where TCommonInput : IParsable
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
        var separtorResult = Separator.Parse(leftResult.Remainder);
        var rightResult = Right.Parse(separtorResult.Remainder);
        return Result.Create(rightResult.Remainder, (leftResult.Output, rightResult.Output));
    }
}

public static class SeparatedPair
{
    public static IParser<TCommonInput, (TLeftOutput, TRightOutput)>
        Create<TCommonInput, TLeftOutput, TSeparatorOutput, TRightOutput>(
            IParser<TCommonInput, TLeftOutput> left,
            IParser<TCommonInput, TSeparatorOutput> separator,
            IParser<TCommonInput, TRightOutput> right)
            where TCommonInput : IParsable
    {
        return new SeparatedPairParser<TCommonInput, TLeftOutput, TSeparatorOutput, TRightOutput>(left, separator, right);
    }
}
