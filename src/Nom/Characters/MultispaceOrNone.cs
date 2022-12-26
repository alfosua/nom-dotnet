using Nom.Sdk;

namespace Nom.Characters;

public interface IMultispaceOrNoneParser<T> : IParser<T, T>
    where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>
{
}

public class MultispaceOrNoneParser<T> : IMultispaceOrNoneParser<T>
    where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>
{
    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.SplitWhenUnsatisfiedOptional<T, char>(input, CommonSatisfiers.IsMultispace);
    }
}

public static class MultispaceOrNone
{
    public static IParser<StringParsable, StringParsable> Create() => new MultispaceOrNoneParser<StringParsable>();
    
    public static IParser<T, T> Create<T>()
        where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>
        => new MultispaceOrNoneParser<T>();
}
