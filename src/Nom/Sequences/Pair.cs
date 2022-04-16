namespace Nom.Sequences;

public interface IPairParser<TCommonInput, TLeftOutput, TRightOutput> : IParser<TCommonInput, (TLeftOutput, TRightOutput)> { }

public class PairParser<TCommonInput, TLeftOutput, TRightOutput> : IPairParser<TCommonInput, TLeftOutput, TRightOutput>
{
    public PairParser(IParser<TCommonInput, TLeftOutput> left, IParser<TCommonInput, TRightOutput> right)
    {
        Left = left;
        Right = right;
    }

    public IParser<TCommonInput, TLeftOutput> Left { get; }
    public IParser<TCommonInput, TRightOutput> Right { get; }

    public IResult<TCommonInput, (TLeftOutput, TRightOutput)> Parse(TCommonInput input)
    {
        var leftResult = Left.Parse(input);
        var rightResult = Right.Parse(leftResult.Remaining);
        return Result.Create(rightResult.Remaining, (leftResult.Output, rightResult.Output));
    }
}

public static class Pair
{

    public static IPairParser<TCommonInput, TLeftOutput, TRightOutput>
        Create<TCommonInput, TLeftOutput, TRightOutput>(
            IParser<TCommonInput, TLeftOutput> left,
            IParser<TCommonInput, TRightOutput> right)
    {
        return new PairParser<TCommonInput, TLeftOutput, TRightOutput>(left, right);
    }
}
