using Nom.Characters;
using Nom.Sequences;
using Xunit;

namespace Nom.Core.UnitTests.Combinations.Characters;

public class DigitCombinationsTests
{
    [Fact]
    public void TestDigitAndSeparatedPair()
    {
        var expected = Result.Create(
            "".AsParsable() with { Offset = 7 },
            ("123".AsParsable(), "123".AsParsable() with { Offset = 4 }));
        var input = "123-123".AsParsable();
        var digit = Digits.Create();
        var chara = Character.Create('-');
        var parser = SeparatedPair.Create(digit, chara, digit);

        var result = parser.Parse(input);

        Assert.Equal(expected, result);
    }
}
