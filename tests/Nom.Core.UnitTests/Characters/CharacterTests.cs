using Nom.Characters;
using Xunit;

namespace Nom.Core.UnitTests.Characters;

public class CharacterTests
{
    [Fact]
    public void CharacterParses_AllCharactersBySingle()
    {
        //@Todo: Implement property-based testing for all characters
        var input = ",".AsParsable();
        var expected = Result.Create("".AsParsable() with { Offset = 1 }, ",".AsParsable());
        var parser = Character.Create(',');

        var result = parser.Parse(input);

        Assert.Equal(expected, result);
    }
}
