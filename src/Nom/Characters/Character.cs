using Nom.Sdk;

namespace Nom.Characters;

public interface ICharacterParser<T> : IParser<T, T>
    where T : IParsable, ISplitableAtPosition<T>, IContentVisitable<char>
{
}

public class CharacterParser<T> : ICharacterParser<T>
    where T : IParsable, ISplitableAtPosition<T>, IContentVisitable<char>
{
    public CharacterParser(char target)
    {
        Target = target;
    }

    public char Target { get; }

    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.ParseByPredicatingNextContent<T, char>(input, c => c == Target, new()
        {
            ExceptionFactory = (_) => new InvalidOperationException($"Could not parse character '{Target}' at head"),
        });
    }
}

public static class Character
{
    public static ICharacterParser<StringParsable> Create(char target) => new CharacterParser<StringParsable>(target);

    public static ICharacterParser<T> Create<T>(char target)
        where T : IParsable, ISplitableAtPosition<T>, IContentVisitable<char>
        => new CharacterParser<T>(target);
}
