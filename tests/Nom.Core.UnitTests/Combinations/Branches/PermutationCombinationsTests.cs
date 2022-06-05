using Nom.Branches;
using Nom.Characters;
using System.Linq;
using Xunit;

namespace Nom.Core.UnitTests.Combinations.Branches;

public class PermutationCombinationsTests
{
    [Fact]
    public void ParsesAlphabetsAndDigitsAtAnyOrder()
    {
        var inputs = new[] { "123abc", "abc123" };
        var expected = new[] { "abc", "123" };

        var parser = Permutation.Create(Alphabetics.Create(), Digits.Create());

        foreach (var input in inputs.Select(x => x.AsParsable()))
        {
            var result = parser.Parse(input);
            Assert.Equal(expected, result.Output.Select(x => (string)x));
            Assert.Equal(string.Empty, result.Remainder);
        }
    }
}
