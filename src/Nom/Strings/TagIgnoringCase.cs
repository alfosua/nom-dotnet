using Nom.Sdk;
using System.Text.RegularExpressions;

namespace Nom.Strings;

public interface ITagIgnoringCaseParser<T> : IParser<T, T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
{
}

public class TagIgnoringCaseParser<T> : ITagIgnoringCaseParser<T>
    where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
{
    public TagIgnoringCaseParser(string target)
    {
        Target = target;
    }

    public string Target { get; }

    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.ParseByMatchingRegex(input, $@"^({Target})", new()
        {
            ExceptionFactory = (_, _) => new InvalidOperationException($"Could not parse tag '{Target}' (ignoring casing) at head"),
            RegexOptions = RegexOptions.IgnoreCase,
        });
    }
}

public static class TagIgnoringCase
{
    public static ITagIgnoringCaseParser<StringParsable> Create(string target) => new TagIgnoringCaseParser<StringParsable>(target);

    public static ITagIgnoringCaseParser<T> Create<T>(string target)
        where T : IParsable, IRegexMatchable, ISplitableAtPosition<T>, IEmptyCheckable
        => new TagIgnoringCaseParser<T>(target);
}
