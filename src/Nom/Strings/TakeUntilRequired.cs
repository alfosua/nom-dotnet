using Nom.Sdk;

namespace Nom.Strings;

public interface ITakeUntilRequiredParser<T> : IParser<T, T>
    where T : IParsable, ISplitableAtPosition<T>, IRegexMatchable
{
}

public class TakeUntilRequiredParser<T> : ITakeUntilRequiredParser<T>
    where T : IParsable, ISplitableAtPosition<T>, IRegexMatchable
{
    public TakeUntilRequiredParser(string pattern)
    {
        Pattern = pattern;
    }

    public string Pattern { get; }

    public IResult<T, T> Parse(T input)
    {
        CommonParsings.CheckInputIsNotEmpty(input);

        var match = input.MatchRegex(Pattern);

        if (match.Success)
        {
            if (match.Index == 0)
            {
                throw new InvalidOperationException("Could not paser anything before the pattern");
            }
            
            var (output, remainder) = input.SplitAtPosition(match.Index);

            return Result.Create(remainder, output);
        }
        else
        {
            throw new InvalidOperationException("Could not matched pattern to parse anything");
        }
    }
}

public static class TakeUntilRequired
{
    public static ITakeUntilRequiredParser<T> Create<T>(string pattern)
        where T : IParsable, ISplitableAtPosition<T>, IRegexMatchable
        => new TakeUntilRequiredParser<T>(pattern);
}
