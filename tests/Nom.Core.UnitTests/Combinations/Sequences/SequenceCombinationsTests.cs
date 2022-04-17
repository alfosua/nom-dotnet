using Nom.Characters;
using Nom.Sequences;
using System.Linq;
using Xunit;

namespace Nom.Core.UnitTests.Combinations.Sequences;

public class SequenceCombinationsTests
{
    [Fact]
    public void ParsesAlphabetsAndDigitsAndAlphabets()
    {
        var input = "abc123def".AsParsable();
        var expected = new[] { "abc", "123", "def" };

        var parser = Sequence.Create(Alphabetics.Create(), Digits.Create(), Alphabetics.Create());

        var result = parser.Parse(input);
        
        Assert.Equal(expected, result.Output.Select(x => x.Content));
        Assert.Equal(string.Empty, result.Remainder.Content);
    }
}
