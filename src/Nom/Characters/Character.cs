using Nom.Sdk;

namespace Nom.Characters;

public interface ICharacterParser : IParser<string, string> { }

public class CharacterParser : ICharacterParser
{
    public CharacterParser(char target)
    {
        Target = target;
    }

    public char Target { get; }

    public IResult<string, string> Parse(string input)
    {
        return CommonParsings.ParseStringByMatchingRegex(input, $@"^{Target}", new()
        {
            ExceptionFactory = (_, _) => new InvalidOperationException($"No character '{Target}' was matched at head"),
        });
    }
}

public static class Character
{
    public static ICharacterParser Create(char target) => new CharacterParser(target);
}
