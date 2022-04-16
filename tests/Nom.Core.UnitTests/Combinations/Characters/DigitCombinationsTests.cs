using Nom.Characters;
using Nom.Sequences;
using Xunit;

namespace Nom.Core.UnitTests.Combinations.Characters;

public class DigitCombinationsTests
{
    [Fact]
    public void TestDigitAndSeparatedPair()
    {
        var digit = Digit.Create();
        var chara = Character.Create('-');
        var parser = SeparatedPair.Create(digit, chara, digit);

        var result = parser.Parse("123-123");

        Assert.Equal(Result.Create("", ("123", "123")), result);
    }
}
