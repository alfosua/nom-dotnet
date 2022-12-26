using Nom.Sdk;

namespace Nom.Characters;

public interface IMultispaceParser<T> : IParser<T, T>
    where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>, IEmptyCheckable
{
}

public class MultispaceParser<T> : IMultispaceParser<T>
    where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>, IEmptyCheckable
{
    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.SplitWhenUnsatisfiedRequired<T, char>(input, CommonSatisfiers.IsMultispace, new()
        {
            ExceptionFactory = (_) => new InvalidOperationException("Could not parse any multiple spacing at head"),
        });
    }
}

public static class Multispace
{
    public static IParser<StringParsable, StringParsable> Create() => new MultispaceParser<StringParsable>();
    
    public static IParser<T, T> Create<T>()
        where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>, IEmptyCheckable
        => new MultispaceParser<T>();
}
