using Nom.Characters;
using Xunit;

namespace Nom.Core.UnitTests.Characters;

public class DigitTests
{
    [Fact]
    public void DigitParsesStringWithOneOrMoreDigits()
    {
        //@Todo: Improve tests
        var input = "123".AsParsable();
        var expected = Result.Create("".AsParsable() with { Offset = 3 }, "123".AsParsable());
        var parser = Digits.Create();

        var result = parser.Parse(input);

        Assert.Equal(expected, result);
    }
}
