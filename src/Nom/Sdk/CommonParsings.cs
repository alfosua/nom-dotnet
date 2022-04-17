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

    public static IResult<TParsable, TParsable> ParseByPredicatingNextContent<TParsable, TContent>(
        TParsable input, Func<TContent, bool> predicate, ParsingOptions<TParsable>? options = null)
        where TParsable : IParsable, ISplitableAtPosition<TParsable>, IContentVisitable<TContent>
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
    
    public static IResult<T, T> ParseByMatchingRegex<T>(
        T input, string pattern, RegexParsingOptions<T>? options = null)
        where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>
    {
        if (options?.ThrowWhenEmptyInput ?? false)
        {
            CheckInputIsNotEmpty(input);
        }

        var match = input.MatchRegex(pattern, options?.RegexOptions ?? RegexOptions.None);

        if (match.Success)
        {
            var (output, remainder) = input.SplitAtPosition(match.Length);

            return Result.Create(remainder, output);
        }
        else
        {
            throw options?.ExceptionFactory?.Invoke(input, match)
                ?? new InvalidOperationException();
        }
    }

    public class ParsingOptions<TParsable>
        where TParsable : IParsable
    {
        public delegate Exception ExceptionCreate(TParsable input);

        public ExceptionCreate? ExceptionFactory { get; set; }

        public bool ThrowWhenEmptyInput { get; set; } = true;
    }

    public class RegexParsingOptions<T>
        where T : IParsable, IRegexMatchable
    {
        public delegate Exception ExceptionCreate(T input, Match regexMatch);

        public ExceptionCreate? ExceptionFactory { get; set; }
        
        public bool ThrowWhenEmptyInput { get; set; } = true;

        public RegexOptions RegexOptions { get; set; } = RegexOptions.None;
    }
}

public static class CommonExceptions
{
    public static InvalidOperationException NotEnoughInputData()
    {
        return new InvalidOperationException("Not enough input data");
    }
}
