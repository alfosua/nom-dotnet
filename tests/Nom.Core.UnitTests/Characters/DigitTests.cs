using Nom.Characters;
using Xunit;

namespace Nom.Core.UnitTests.Characters;

public class DigitTests
{
    [Fact]
    public void DigitParsesStringWithOneOrMoreDigits()
    {
        var parser = Digit.Create();

        var result = parser.Parse("123");

        Assert.Equal(Result.Create("", "123"), result);
    }
}
