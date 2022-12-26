namespace Nom.Strings;

public interface ITagParser<T> : IParser<T, T>
    where T : IParsable, ISplitableAtPosition<T>, IEmptyCheckable, IEquatable<string>
{
}

public class TagParser<T> : ITagParser<T>
    where T : IParsable, ISplitableAtPosition<T>, IEmptyCheckable, IEquatable<string>
{
    public TagParser(string target)
    {
        Target = target;
    }

    public string Target { get; }

    public IResult<T, T> Parse(T input)
    {
        var (output, remainder) = input.SplitAtPosition(Target.Length);

        if (!output.Equals(Target))
        {
            throw new InvalidOperationException($"Could not parser tag '{Target}' at head");
        }

        return Result.Create(remainder, output);
    }
}

public static class Tag
{
    public static IParser<StringParsable, StringParsable> Create(string target) => new TagParser<StringParsable>(target);

    public static IParser<T, T> Create<T>(string target)
        where T : IParsable, ISplitableAtPosition<T>, IEmptyCheckable, IEquatable<string>
        => new TagParser<T>(target);
}
