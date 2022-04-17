namespace Nom.Sequences;

public interface IPairParser<TCommonInput, TLeftOutput, TRightOutput>
    : IParser<TCommonInput, (TLeftOutput, TRightOutput)>
    where TCommonInput : IParsable
{
}

public class PairParser<TCommonInput, TLeftOutput, TRightOutput> : IPairParser<TCommonInput, TLeftOutput, TRightOutput>
    where TCommonInput : IParsable
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
        var rightResult = Right.Parse(leftResult.Remainder);
        return Result.Create(rightResult.Remainder, (leftResult.Output, rightResult.Output));
    }
}

public static class Pair
{

    public static IPairParser<TCommonInput, TLeftOutput, TRightOutput>
        Create<TCommonInput, TLeftOutput, TRightOutput>(
            IParser<TCommonInput, TLeftOutput> left,
            IParser<TCommonInput, TRightOutput> right)
            where TCommonInput : IParsable
    {
        return new PairParser<TCommonInput, TLeftOutput, TRightOutput>(left, right);
    }
}
