using FsCheck.Xunit;
using Nom.Characters;
using Xunit;

namespace Nom.Core.UnitTests.Characters;

public class CharacterTests
{
    [Property]
    public void ParsesEveryChar(char c)
    {
        var input = c.ToString();
        
        var result = Character.Create(c).Parse(input);
    }
}
