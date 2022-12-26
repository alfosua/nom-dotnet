namespace Nom.Strings;

public interface ITagIgnoringCaseParser<T> : IParser<T, T>
    where T : IParsable, ISplitableAtPosition<T>, IEmptyCheckable, IEquatableIgnoringCase<string>
{
}

public class TagIgnoringCaseParser<T> : ITagIgnoringCaseParser<T>
    where T : IParsable, ISplitableAtPosition<T>, IEmptyCheckable, IEquatableIgnoringCase<string>
{
    public TagIgnoringCaseParser(string target)
    {
        Target = target.ToLower();
    }

    public string Target { get; }

    public IResult<T, T> Parse(T input)
    {
        var (output, remainder) = input.SplitAtPosition(Target.Length);

        if (!output.EqualsIgnoringCase(Target))
        {
            throw new InvalidOperationException($"Could not parser tag '{Target}' ignoring case at head");
        }

        return Result.Create(remainder, output);
    }
}

public static class TagIgnoringCase
{
    public static IParser<StringParsable, StringParsable> Create(string target) => new TagIgnoringCaseParser<StringParsable>(target);

    public static IParser<T, T> Create<T>(string target)
        where T : IParsable, ISplitableAtPosition<T>, IEmptyCheckable, IEquatableIgnoringCase<string>
        => new TagIgnoringCaseParser<T>(target);
}
