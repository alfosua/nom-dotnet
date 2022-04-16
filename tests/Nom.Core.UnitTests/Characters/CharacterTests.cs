using Nom.Characters;
using Xunit;

namespace Nom.Core.UnitTests.Characters;

public class CharacterTests
{
    [Fact]
    public void CharacterParses_AllCharactersBySingle()
    {
        var parser = Character.Create(',');

        var result = parser.Parse(",");

        Assert.Equal(Result.Create("", ","), result);
    }
}
