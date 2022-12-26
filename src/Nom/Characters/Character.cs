using Nom.Sdk;

namespace Nom.Characters;

public interface ICharacterParser<T> : IParser<T, T>
    where T : IParsable, ISplitableAtPosition<T>, IContentVisitable<char>, IEmptyCheckable
{
}

public class CharacterParser<T> : ICharacterParser<T>
    where T : IParsable, ISplitableAtPosition<T>, IContentVisitable<char>, IEmptyCheckable
{
    public CharacterParser(char target)
    {
        Target = target;
    }

    public char Target { get; }

    public IResult<T, T> Parse(T input)
    {
        return CommonParsings.SplitAtNextIfSatisfied<T, char>(input, c => c == Target, new()
        {
            ExceptionFactory = (_) => new InvalidOperationException($"Could not parse character '{Target}' at head"),
        });
    }
}

public static class Character
{
    public static IParser<StringParsable, StringParsable> Create(char target) => new CharacterParser<StringParsable>(target);

    public static IParser<T, T> Create<T>(char target)
        where T : IParsable, ISplitableAtPosition<T>, IContentVisitable<char>, IEmptyCheckable
        => new CharacterParser<T>(target);
}
