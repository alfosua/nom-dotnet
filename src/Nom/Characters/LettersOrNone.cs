using Nom.Sdk;

namespace Nom.Characters;

public interface ILettersOrNoneParser<T> : IParser<T, T>
    where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>
{
}

public class LettersOrNoneParser<T> : ILettersOrNoneParser<T>
    where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>
{
    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.SplitWhenUnsatisfiedOptional<T, char>(input, char.IsLetter);
    }
}

public static class LettersOrNone
{
    public static IParser<StringParsable, StringParsable> Create() => new LettersOrNoneParser<StringParsable>();
    
    public static IParser<T, T> Create<T>()
        where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>
        => new LettersOrNoneParser<T>();
}
