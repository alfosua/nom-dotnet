using System.Text.RegularExpressions;

namespace Nom.Sdk;

public static class CommonParsings
{
    public static IResult<string, string> ParseStringByMatchingRegex(string input, string pattern, ParsingOptions? options = null)
    {
        var match = Regex.Match(input, pattern);

        if (match.Success)
        {
            var nextInput = input.Substring(match.Length);

            var result = match.Value;

            return Result.Create(nextInput, result);
        }
        else
        {
            throw options?.ExceptionFactory?.Invoke(input, match)
                ?? new InvalidOperationException();
        }
    }

    public class ParsingOptions
    {
        public ExceptionCreate? ExceptionFactory { get; set; }

        public delegate Exception ExceptionCreate(string input, Match regexMatch);
    }
}
