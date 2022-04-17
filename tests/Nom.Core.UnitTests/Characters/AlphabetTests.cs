using Nom.Characters;
using Xunit;

namespace Nom.Core.UnitTests.Characters;

public class AlphabetTests
{
    [Fact]
    public void AlphabetParsesAlphabetics()
    {
        var input = "abc1abc".AsParsable();
        var expected = Result.Create("1abc".AsParsable() with { Offset = 3 }, "abc".AsParsable());
        var parser = Alphabetics.Create();

        var result = parser.Parse(input);

        Assert.Equal(expected, result);
    }
}
