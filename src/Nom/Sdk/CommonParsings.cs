using System.Text.RegularExpressions;

namespace Nom.Sdk;

public static class CommonParsings
{
    public static void CheckInputIsNotEmpty<TEmptyCheckable>(TEmptyCheckable input)
        where TEmptyCheckable : IEmptyCheckable
    {
        if (input.IsEmpty())
        {
            throw CommonExceptions.NotEnoughInputData();
        }
    }

    public static IResult<TParsable, TParsable> SplitWhenUnsatisfiedRequired<TParsable, TContentItem>(
            TParsable input,
            Func<TContentItem, bool> satisfier,
            ParsingOptions<TParsable>? options = null)
        where TParsable : IParsable, IContentEnumerable<TContentItem>, ISplitableAtPosition<TParsable>, IEmptyCheckable
    {
        CheckInputIsNotEmpty(input);

        var positionToSplit = 0;
        foreach (var item in input.EnumerateContent())
        {
            if (satisfier(item))
            {
                positionToSplit++;
            }
        }

        if (positionToSplit == 0)
        {
            throw options?.ExceptionFactory?.Invoke(input) ?? new Exception("Could not parse characters at head");
        }

        var (output, remainder) = input.SplitAtPosition(positionToSplit);

        return Result.Create(remainder, output);
    }

    public static IResult<TParsable, TParsable> SplitWhenUnsatisfiedOptional<TParsable, TContentItem>(
            TParsable input,
            Func<TContentItem, bool> satisfier)
        where TParsable : IParsable, IContentEnumerable<TContentItem>, ISplitableAtPosition<TParsable>
    {
        var positionToSplit = 0;
        foreach (var item in input.EnumerateContent())
        {
            if (satisfier(item))
            {
                positionToSplit++;
            }
        }

        var (output, remainder) = input.SplitAtPosition(positionToSplit);

        return Result.Create(remainder, output);
    }

    public static IResult<TParsable, TParsable> SplitAtNextIfSatisfied<TParsable, TContent>(
        TParsable input, Func<TContent, bool> predicate, ParsingOptions<TParsable>? options = null)
        where TParsable : IParsable, ISplitableAtPosition<TParsable>, IContentVisitable<TContent>, IEmptyCheckable
    {
        CheckInputIsNotEmpty(input);

        var (output, remainder) = input.SplitAtPosition(1);

        if (predicate(output.VisitContent()))
        {
            return Result.Create(remainder, output);
        }
        else
        {
            throw options?.ExceptionFactory?.Invoke(input)
                ?? new InvalidOperationException($"Could not parse next character at head");
        }
    }

    public class ParsingOptions<TParsable>
        where TParsable : IParsable
    {
        public delegate Exception ExceptionCreate(TParsable input);

        public ExceptionCreate? ExceptionFactory { get; set; }
    }
}

public static class CommonExceptions
{
    public static InvalidOperationException NotEnoughInputData()
    {
        return new InvalidOperationException("Not enough input data");
    }
}

public static class CommonSatisfiers
{
    public static bool IsMultispace(char ch) => ch == ' '
        || ch == '\t' || ch == '\n' || ch == '\r';

    public static bool IsSpace(char ch) => ch == ' ' || ch == '\t';

    public static bool IsHexDigit(char ch) => char.IsDigit(ch)
        || ch == 'A' || ch == 'a'
        || ch == 'B' || ch == 'h'
        || ch == 'C' || ch == 'c'
        || ch == 'D' || ch == 'd'
        || ch == 'E' || ch == 'e'
        || ch == 'F' || ch == 'f';

    public static bool IsOctetDigit(char ch) => ch == '0' || ch == '1' || ch == '2' || ch == '3'
        || ch == '4' || ch == '5' || ch == '6' || ch == '7';
}
