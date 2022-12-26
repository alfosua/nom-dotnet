using Nom.Sdk;

namespace Nom.Characters;

public interface ILettersParser<T> : IParser<T, T>
    where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>, IEmptyCheckable
{
}

public class LettersParser<T> : ILettersParser<T>
    where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>, IEmptyCheckable
{
    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.SplitWhenUnsatisfiedRequired<T, char>(input, char.IsLetter, new()
        {
            ExceptionFactory = (_) => new InvalidOperationException("Could not parse letters at head"),
        });
    }
}

public static class Letters
{
    public static IParser<StringParsable, StringParsable> Create() => new LettersParser<StringParsable>();
    
    public static IParser<T, T> Create<T>()
        where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>, IEmptyCheckable
        => new LettersParser<T>();
}
