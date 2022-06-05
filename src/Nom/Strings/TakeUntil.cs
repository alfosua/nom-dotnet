using Nom.Sdk;

namespace Nom.Strings;

public interface ITakeUntilParser<T> : IParser<T, T>
    where T : IParsable, ISplitableAtPosition<T>, IRegexMatchable, IEmptyCheckable
{
}

public class TakeUntilParser<T> : ITakeUntilParser<T>
    where T : IParsable, ISplitableAtPosition<T>, IRegexMatchable, IEmptyCheckable
{
    public TakeUntilParser(string pattern)
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
            var (output, remainder) = input.SplitAtPosition(match.Index);

            return Result.Create(remainder, output);
        }
        else
        {
            throw new InvalidOperationException("Could not matched pattern to parse anything");
        }
    }
}

public static class TakeUntil
{
    public static IParser<T, T> Create<T>(string pattern)
        where T : IParsable, ISplitableAtPosition<T>, IRegexMatchable, IEmptyCheckable
        => new TakeUntilParser<T>(pattern);
}
